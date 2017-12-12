import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { Student } from '../dto/student';
import { StudentService } from '../services/student.service';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  // @Input() student: Student;

  constructor(    
    private studentService: StudentService,
    public dialogRef: MatDialogRef<StudentComponent>,
    @Inject(MAT_DIALOG_DATA) public student: any) { }

  ngOnInit() {
  }

  getStudent(id: number): void {
    this.studentService.getStudent(id).subscribe( student => this.student = student );
  }

  close(): void{
    this.dialogRef.close();
  }

}
