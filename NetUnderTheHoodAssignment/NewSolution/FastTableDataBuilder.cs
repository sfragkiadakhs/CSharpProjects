using CsvDataAccess.CsvReading;
using CsvDataAccess.Interface;

namespace CsvDataAccess.NewSolution;

public class FastTableDataBuilder : ITableDataBuilder
{
    public ITableData Build(CsvData csvData)
    {
        var resultRows = new List<FastRow>();

        foreach (var row in csvData.Rows)
        {
            var newRaw = new FastRow();


            for (int columnIndex = 0; columnIndex < csvData.Columns.Length; ++columnIndex)
            {
                var column = csvData.Columns[columnIndex];
                string valueAsString = row[columnIndex];

                if (string.IsNullOrEmpty(valueAsString))
                {
                    continue;
                }
                else if (valueAsString == "TRUE")
                {
                    newRaw.AssignBool(column, true);
                }
                else if (valueAsString == "FALSE")
                {
                    newRaw.AssignBool(column, false);
                }
                else if (valueAsString.Contains(".") && decimal.TryParse(valueAsString, out var valueAsDecimal))
                {
                    newRaw.AssignDecimal(column, valueAsDecimal);
                }
                else if (int.TryParse(valueAsString, out var valueAsInt))
                {
                    newRaw.AssignInt(column, valueAsInt);
                }
                else
                {
                    newRaw.AssignString(column, valueAsString);

                }
            }

            resultRows.Add(newRaw);
        }

        return new FastTableData(csvData.Columns, resultRows);
    }


}
