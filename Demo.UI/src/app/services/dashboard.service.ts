import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { YearlyEnrollmentFact } from '../dto/yearlyEnrollmentFact';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class DashboardService {

  constructor(private http: HttpClient) { }

  getEnrolledStudents(): Observable<number> {
    return this.http.get<number>('/api/dashboard/getenrolledstudentcount');
  }

  getNumberOfTeachers(): Observable<number> {
    return this.http.get<number>('/api/dashboard/GetNumberOfTeachers');
  }

  getTeacherStudentRatio(): Observable<Ratio> {
    return this.http.get<Ratio>('/api/dashboard/GetTeacherStudentRatio');
  }

  getYearlyEnrollmentFacts(): Observable<YearlyEnrollmentFact[]> {
    return this.http.get<YearlyEnrollmentFact[]>('api/dashboard/GetEnrollmentStatistics');
  }
}

export class Ratio {
  numerator: number;
  denominator: number;
}