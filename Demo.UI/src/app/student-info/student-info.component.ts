import { Component, OnInit } from '@angular/core';
import { Student } from '../dto/student';

@Component({
  selector: 'app-student-info',
  templateUrl: './student-info.component.html',
  styleUrls: ['./student-info.component.css']
})
export class StudentInfoComponent implements OnInit {

  student: Student;

  constructor() { }

  ngOnInit() {
    this.student = new Student;
  }

}
