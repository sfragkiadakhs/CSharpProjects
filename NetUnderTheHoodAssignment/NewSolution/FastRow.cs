namespace CsvDataAccess.NewSolution;

public class FastRow
{
    private Dictionary<string, bool> bool_data = new();
    private Dictionary<string, string> string_data = new();
    private Dictionary<string, int> int_data = new();
    private Dictionary<string, decimal> decimal_data = new();

    public object GetAtColumn(string columnName)
    {
        if (bool_data.ContainsKey(columnName))
        {
            return bool_data[columnName];  
        }
        if (string_data.ContainsKey(columnName)) 
        {
            return string_data[columnName]; 
        }
        if (int_data.ContainsKey(columnName))
        {
            return int_data[columnName];
        }
        if(decimal_data.ContainsKey(columnName)) 
        {
            return decimal_data[columnName]; 
        }
        return null;
    }

    public void AssignBool(string columnName, bool value)
    {
        bool_data[columnName] = value;
    }
        
    public void AssignString(string columnName, string value)
    {
        string_data[columnName] = value;
    }

    public void AssignInt(string columnName,int value)
    {
        int_data[columnName] = value;
    }
        
    public void AssignDecimal(string columnName, decimal value)
    {
        decimal_data[columnName] = value;
    }
}

