import { Injectable } from '@angular/core';
import { Teacher } from '../dto/teacher';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class TeacherService {

  constructor(private http: HttpClient) { }

  getTeachers(): Observable<Teacher[]> {
    return this.http.get<Teacher[]>('/api/teacher/getteachers');
  }

  addTeacher(teacher: Teacher) : Observable<Teacher> {
    return this.http.post<Teacher>('/api/teacher/createteacher', teacher, httpOptions);
  }

}
