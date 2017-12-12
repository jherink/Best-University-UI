import { Component, OnInit } from '@angular/core';
import { DashboardService, Ratio } from '../services/dashboard.service';

// export var single = [
//   {
//     "name": "Annual Student Enrollment",
//     "series": [
//       {
//       }
//     ]
//   }
// ];

export var single = [
  {
    "name": "Germany",
    "value": 8940000
  },
  {
    "name": "USA",
    "value": 5000000
  },
  {
    "name": "France",
    "value": 7200000
  }
];

export var multi = [
  {
    "name": "Students Enrolled",
    "series": []
  }
  
];

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  enrolledStudents: number;
  employedTeachers: number;
  studentRatio: Ratio;

  constructor(private dashboardService: DashboardService) {
    // this.studentRatio = new Ratio();
    // this.enrolledStudents = 0;
    // this.employedTeachers = 0;
    // Object.assign(this, { single });
      Object.assign(this, {single, multi})  
      
  }

  ngOnInit() {
    this.refresh();
  }

  single: any[];
  multi: any[];

  view: any[] = [700, 400];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = false;
  showXAxisLabel = true;
  xAxisLabel = 'Year';
  showYAxisLabel = true;
  yAxisLabel = 'Students Enrolled ';
  isLoadingResults = false;

  colorScheme = {
    domain: ['#3f51b5']
  };

  // line, area
  autoScale = true;

  xAxisTickFormatting(val: number): string {
    return val.toString();
  }

  onSelect(event) {
    console.log(event);
  }

  refresh(): void {
    this.isLoadingResults = true;
    this.dashboardService.getEnrolledStudents().subscribe(n => this.enrolledStudents = n);
    this.dashboardService.getNumberOfTeachers().subscribe(n => this.employedTeachers = n);
    this.dashboardService.getTeacherStudentRatio().subscribe(n => this.studentRatio = n);
    this.dashboardService.getYearlyEnrollmentFacts().subscribe(t => {
      this.isLoadingResults = false;
      
      this.multi[0].series = [];
      for (let i = 0; i < t.length; i++) {
        this.multi[0].series.push({ "name": t[i].year, "value": t[i].students });        
      }

      this.multi = [...this.multi];
    });
  }

}
