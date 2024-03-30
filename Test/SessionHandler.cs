using Newtonsoft.Json;

public class SessionHandler : SessionBasics
{

  public object GetValue(
      HttpContext context, string key
    )
  {
    return Utils.GetPropertyValue(
      Utils.GetPropertyValue(Retrieve(context), "data"), key
    );
  }

  public void SetValue(
    HttpContext context, string key, object value
  )
  {
    var retrieved = Retrieve(context);
    var data = Utils.GetPropertyValue(retrieved, "data");
    var created = Utils.GetPropertyValue(retrieved, "created");
    Utils.SetProperty(data, key, value);
    var parameters = new List<Object>()
      {"data",  JsonConvert.SerializeObject(data)};
    string sql;
    var cookieValue = Utils.GetPropertyValue(retrieved, "id");
    parameters.Add("id");
    parameters.Add(cookieValue);
    if (created != null)
    {
      sql = @"UPDATE sessions 
              SET modified=DATETIME('now'), data=$data 
              WHERE id=$id";
    }
    else
    {
      sql = @"INSERT INTO sessions(id, data) 
              VALUES($id,$data)";
    }
    SQLQuery.RunOne(sql, parameters.ToArray());
  }

  // Only call once for the whole app
  public static async void DeleteOldSessions(int timeToLiveHours)
  {
    while (true)
    {
      SQLQuery.RunOne(
        @$"DELETE FROM sessions WHERE 
           DATETIME('now', '-{timeToLiveHours} hours') > modified"
      );
      await Task.Delay(60000); // check one a minute
    }
  }

}