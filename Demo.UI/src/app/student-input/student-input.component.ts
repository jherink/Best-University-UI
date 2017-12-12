import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { Student } from '../dto/student';
import { Address } from '../dto/address';
import { StudentService } from '../services/student.service';

import { FormControl } from '@angular/forms';
import { RequiredInputMatcher } from '../validators/required-input';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-student-input',
  templateUrl: './student-input.component.html',
  styleUrls: ['./student-input.component.css']
})
export class StudentInputComponent implements OnInit {
  student: Student;

  startDate: Date = new Date(1990, 0, 1);

  ematcher = new RequiredInputMatcher();

  firstNameControl = new FormControl('', [Validators.required])
  middleNameControl = new FormControl('', [Validators.required])
  lastNameControl = new FormControl('', [Validators.required])
  address1Control = new FormControl('', [Validators.required])
  address2Control = new FormControl('', [Validators.required])
  zipCodeControl = new FormControl('', [Validators.required])
  telephoneControl = new FormControl('', [Validators.required])
  dobControl = new FormControl('', [Validators.required])

  controls: FormControl[];

  constructor(private studentService: StudentService,
    private snackBar: MatSnackBar) {
    this.student = new Student;
    this.student.address = new Address;

    this.controls = [
      this.firstNameControl, this.middleNameControl, this.lastNameControl,
      this.address1Control, this.address2Control, this.zipCodeControl,
      this.telephoneControl, this.dobControl
    ];
  }

  ngOnInit() {
  }

  enrollStudent(): void {
    console.log(this.firstNameControl.valid);
    console.log(this.firstNameControl.invalid);
    console.log(this.firstNameControl.value);
    console.log(this.ematcher.isErrorState(this.firstNameControl, null));

    let flag = true;
    for (let control of this.controls) {
      // true when control has been touched and is not in an error state.
      flag = !this.ematcher.isErrorState(control, null) && control.touched;
      if (!flag) break;
    }

    if (!flag) {
      this.openSnackBar('One or more inputs required!', 'Dismiss');
    }
    else {
      // this.openSnackBar('good!', 'Dismiss');

      this.studentService.enrollStudent(this.student)
        .subscribe(
        res => {
          console.log(res);
          this.openSnackBar(`${res.firstName} ${res.lastName} added succesfully!`, 'Dismiss');
        },
        err => {
          console.log('error occured');
          this.openSnackBar(`${this.student.firstName} ${this.student.lastName} failed to be added`, 'Dismiss');
        });
    }
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

}
