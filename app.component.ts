

import { Component, OnInit } from '@angular/core';
import { EmployeeService, Employee } from './employee.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  employees: Employee[] = [];
  chartLabels: string[] = [];
  chartData: number[] = [];

  constructor(private empService: EmployeeService) {}

  ngOnInit() {
    this.empService.getEmployees().subscribe(data => {
      this.employees = data.sort((a, b) => b.hoursWorked - a.hoursWorked);
      const total = this.employees.reduce((sum, emp) => sum + emp.hoursWorked, 0);
      this.chartLabels = this.employees.map(emp => emp.name);
      this.chartData = this.employees.map(emp => +((emp.hoursWorked / total) * 100).toFixed(2));
    });
  }
}
