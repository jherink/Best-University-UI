import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

import { Student } from '../dto/student';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable()
export class StudentService {

  constructor(private http: HttpClient) { }

  getStudent(id: number): Observable<Student> {
    return this.http.get<Student>(`/api/student/GetStudent?id=${id}`);
  }

  search(firstName: string, lastName: string): Observable<Student[]> {
    console.log(firstName + " " + lastName );
    return this.http.get<Student[]>(`/api/student/search?firstName=${firstName}&lastName=${lastName}`);
  }

  enrollStudent(student: Student): Observable<Student> {
    console.log(student);
    return this.http.post<Student>('/api/student/enrollstudent', student, httpOptions);
    // .subscribe(
    //   res => {
    //     console.log(res);
    //     return true;
    //   },
    //   err => {
    //   console.log('error occured');
    // });
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
 
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
 
      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);
 
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
