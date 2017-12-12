import { Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { TeacherService } from '../services/teacher.service';
import { Teacher } from '../dto/teacher';

import { of } from 'rxjs/observable/of';
import { startWith } from 'rxjs/operators/startWith';
import { merge } from 'rxjs/observable/merge';
import { switchMap } from 'rxjs/operators/switchMap';
import { map } from 'rxjs/operators/map';
import { catchError } from 'rxjs/operators/catchError';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.css']
})
export class TeachersComponent implements OnInit {
  displayedColumns = ['name', 'title', 'email', 'department'];
  dataSource = new MatTableDataSource();

  isLoadingResults = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private teacherService: TeacherService) {
  }

  ngOnInit() {
  }

  /**
   * Set the paginator and sort after the view init since this component will
   * be able to query its view for the initialized paginator and sort.
   */
  ngAfterViewInit() {
    merge().pipe(
      startWith({}),
      switchMap(() => {
        this.isLoadingResults = true;
        return this.teacherService.getTeachers();
      }),
      map(data => {
        this.isLoadingResults = false;
        return data;
      }),
      catchError(()=> {
        this.isLoadingResults = false;
        return of([]);
      })
    ).subscribe(data => {
      this.dataSource.data = data;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
}