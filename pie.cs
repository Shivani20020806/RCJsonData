using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
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

        float total = (float)grouped.Sum(e => e.TotalHours);
        int width = 600, height = 600;
        Bitmap bitmap = new Bitmap(width, height);
        Graphics g = Graphics.FromImage(bitmap);
        g.Clear(Color.White);

        float startAngle = 0;
        Random rand = new Random();

        foreach (var emp in grouped)
        {
            float sweepAngle = (float)(emp.TotalHours / total * 360);
            Color color = Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
            Brush brush = new SolidBrush(color);

            g.FillPie(brush, 50, 50, 500, 500, startAngle, sweepAngle);
            startAngle += sweepAngle;
        }

        bitmap.Save("piechart.png", ImageFormat.Png);
        Console.WriteLine("Pie chart saved as piechart.png");
    }
}
