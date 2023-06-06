namespace TrackAndFieldUtility.Data;
using SQLite;
using objects;


public class DBAccess
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteAsyncConnection conn;

    public DBAccess(string dbPath)
    {
        _dbPath = dbPath;
    }

    private async Task InitAsync()
    {

        // Don't Create database if it exists

        if (conn != null)

            return;

        // Create database and WeatherForecast Table

        conn = new SQLiteAsyncConnection(_dbPath);

        await conn.CreateTableAsync<PointsPerEvent>();

    }

    public async Task<List<PointsPerEvent>> GetForecastAsync()

    {

        await InitAsync();

        return await conn.Table<PointsPerEvent>().ToListAsync();

    }

    public async Task<int> FillTable(List<PointsPerEvent> objects)
    {
        try
        {

            await InitAsync();
            //await conn.DeleteAllAsync<PointsPerEvent>();
            //foreach (var item in objects)
            //{
            //    await conn.InsertAsync(item);
            //}
            for (int i = 0; i < 4; i++)
            {
                await conn.InsertAsync(objects[i]);
            }
            return objects.Count;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<PointsPerEvent> GetEvent(int id)
    {
        try
        {
            await InitAsync();
            return await conn.Table<PointsPerEvent>().Where(x=> x.Event == "100m" && x.Points == 1400).FirstOrDefaultAsync();

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<PointsPerEvent> UpdateForecastAsync(PointsPerEvent paramWeatherForecast)
    {
        await InitAsync();
        // Update
        await conn.UpdateAsync(paramWeatherForecast);
        // Return the updated object
        return paramWeatherForecast;
    }

    public async Task<PointsPerEvent> DeleteForecastAsync(PointsPerEvent paramWeatherForecast)
    {
        // Delete
        await conn.DeleteAsync(paramWeatherForecast);
        return paramWeatherForecast;
    }

}

