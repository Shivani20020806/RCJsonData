# RCJsonData

This project showcases frontend and backend skills including:
API integration
Data processing and grouping
HTML table styling

Chart visualization in both web and image form

Each record contains:
- `name`: Employee name
- `startTime`: Start of time entry
- `endTime`: End of time entry



##  Task 1 – Angular (Frontend)

### a) Visualize JSON Data in a Table
- Data is fetched using `HttpClient` from the given API.
- Employees are grouped by name, and total hours are calculated.
- Table is sorted by total hours worked (descending).
- Rows are highlighted in **red** if hours worked < 100.

### b) Visualize JSON Data in a Pie Chart
- Uses `ng2-charts` and `chart.js`.
- Pie chart shows the **percentage of total hours worked** by each employee.


 Task 2 – C# Console App (Backend)
a) Generate HTML Table
A console app fetches the same API.

Groups data by employee and calculates total hours.

Creates an HTML file output.html showing the table.

Rows where hours < 100 are highlighted in red.

b) Generate Pie Chart Image
A chart.png image is generated to show a pie chart.

Represents the percentage of total hours worked per employee.

Uses System.Drawing or ScottPlot/OxyPlot .


