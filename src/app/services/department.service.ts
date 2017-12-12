import { Injectable } from '@angular/core';
import { Department } from '../dto/department';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable()
export class DepartmentService {

  constructor(private http: HttpClient) { }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>('/api/department/getdepartments');
  }

}
