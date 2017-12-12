import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { StudentService } from '../services/student.service';
import { Student } from '../dto/student';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { of } from 'rxjs/observable/of';
import { startWith } from 'rxjs/operators/startWith';
import { merge } from 'rxjs/observable/merge';
import { switchMap } from 'rxjs/operators/switchMap';
import { map } from 'rxjs/operators/map';
import { catchError } from 'rxjs/operators/catchError';
import { StudentComponent } from '../student/student.component';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})
export class StudentSearchComponent implements OnInit {

  studentId: number;
  searchFirstName: string = '';
  searchLastName: string = '';
  activeStudent: Student;
  isSearchingStudentID: boolean = false;
  isSearchingStudent: boolean = false;
  
  displayedColumns = ['firstName', 'lastName', 'details'];
  dataSource = new MatTableDataSource<Student>();


  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private studentService: StudentService,
    public dialog: MatDialog) { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getStudentById(): void {
    this.isSearchingStudentID = true;
    this.studentService.getStudent(this.studentId).subscribe(t => {
      this.isSearchingStudentID = false;
      this.activeStudent = t;
      this.openStudentInfoDialog();    
    });
  }

  selectStudent(student: Student): void {
    this.activeStudent = student;
    this.openStudentInfoDialog();
  }

  animal: string;
  name: string;

  openStudentInfoDialog(): void {
    let dialogRef = this.dialog.open(StudentComponent, {
      // width: '500px',
      data: this.activeStudent
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.animal = result;
    });
  }

  getStudentByName(): void {

    this.isSearchingStudent = true;
    this.studentService.search(this.searchFirstName, this.searchLastName)
      .subscribe(students => {
        console.log(students);
        if (students.length == 1) {
          this.activeStudent = students[0];
        }
        else {
          // this.searchedStudents = students;
          this.dataSource.data = students;
          this.activeStudent = null;
        }
        this.isSearchingStudent = false;
      }
      );
  }

}

// @Component({
//   selector: 'dialog-overview-example-dialog',
//   templateUrl: 'dialog-overview-example-dialog.html',
// })
// export class DialogOverviewExampleDialog {

//   constructor(
//     public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
//     @Inject(MAT_DIALOG_DATA) public data: any) { }

//   onNoClick(): void {
//     this.dialogRef.close();
//   }
// }