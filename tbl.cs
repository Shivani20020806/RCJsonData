using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TimeEntry
{
    public string EmployeeName { get; set; }
    public double HoursWorked { get; set; }
}

class Program
{
    static async Task Main()
    {
        string url = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

        using HttpClient client = new HttpClient();
        var response = await client.GetStringAsync(url);

        var timeEntries = JsonSerializer.Deserialize<List<TimeEntry>>(response);

        var grouped = timeEntries
            .GroupBy(e => e.EmployeeName)
            .Select(g => new
            {
                Name = g.Key,
                TotalHours = g.Sum(x => x.HoursWorked)
            })
            .OrderByDescending(e => e.TotalHours)
            .ToList();

        using StreamWriter writer = new StreamWriter("employees.html");

        writer.WriteLine("<html><head><style>");
        writer.WriteLine("table { border-collapse: collapse; width: 50%; }");
        writer.WriteLine("th, td { border: 1px solid black; padding: 8px; text-align: left; }");
        writer.WriteLine(".low-hours { background-color: #f8d7da; }");
        writer.WriteLine("</style></head><body>");
        writer.WriteLine("<h2>Employee Work Hours</h2>");
        writer.WriteLine("<table>");
        writer.WriteLine("<tr><th>Name</th><th>Total Hours Worked</th></tr>");

        foreach (var emp in grouped)
        {
            string rowClass = emp.TotalHours < 100 ? " class='low-hours'" : "";
            writer.WriteLine($"<tr{rowClass}><td>{emp.Name}</td><td>{emp.TotalHours:F2}</td></tr>");
        }

        writer.WriteLine("</table></body></html>");
        Console.WriteLine("HTML table created: employees.html");
    }
}
