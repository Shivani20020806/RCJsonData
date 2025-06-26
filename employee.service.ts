

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

export interface TimeEntry {
  name: string;
  startTime: string;
  endTime: string;
}

export interface Employee {
  name: string;
  hoursWorked: number;
}

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private apiUrl = 'https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==';

  constructor(private http: HttpClient) {}

  getEmployees(): Observable<Employee[]> {
    return this.http.get<TimeEntry[]>(this.apiUrl).pipe(
      map((entries: TimeEntry[]) => {
        const employeeMap: { [key: string]: number } = {};

        entries.forEach(entry => {
          const start = new Date(entry.startTime).getTime();
          const end = new Date(entry.endTime).getTime();
          const hours = (end - start) / (1000 * 60 * 60); // convert ms to hours

          if (!employeeMap[entry.name]) {
            employeeMap[entry.name] = 0;
          }
          employeeMap[entry.name] += hours;
        });

        return Object.entries(employeeMap).map(([name, hoursWorked]) => ({
          name,
          hoursWorked: +hoursWorked.toFixed(2),
        }));
      })
    );
  }
}

