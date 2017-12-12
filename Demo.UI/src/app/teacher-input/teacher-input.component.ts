import { Component, OnInit } from '@angular/core';
import { Teacher } from '../dto/teacher';
import { TeacherService } from '../services/teacher.service';
import { MatSnackBar, ErrorStateMatcher } from '@angular/material';
import { Department } from '../dto/department';
import { Address } from '../dto/address';
import { DepartmentService } from '../services/department.service';
import { FormControl } from '@angular/forms';
import { RequiredInputMatcher } from '../validators/required-input';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-teacher-input',
  templateUrl: './teacher-input.component.html',
  styleUrls: ['./teacher-input.component.css']
})
export class TeacherInputComponent implements OnInit {

  teacher: Teacher;
  departments: Department[];

  startDate: Date = new Date(1990, 0, 1);
  ematcher = new RequiredInputMatcher();

  firstNameControl = new FormControl('', [Validators.required])
  middleNameControl = new FormControl('', [Validators.required])
  lastNameControl = new FormControl('', [Validators.required])
  titleControl = new FormControl('', [Validators.required])
  departmentControl = new FormControl('', [Validators.required])
  address1Control = new FormControl('', [Validators.required])
  address2Control = new FormControl('', [Validators.required])
  zipCodeControl = new FormControl('', [Validators.required])
  telephoneControl = new FormControl('', [Validators.required])
  dobControl = new FormControl('', [Validators.required])

  controls: FormControl[];


  constructor(private teacherService: TeacherService,
    private departmentService: DepartmentService,
    private snackBar: MatSnackBar) {

    this.teacher = new Teacher();
    this.teacher.address = new Address();
    this.teacher.department = new Department();

    this.controls = [
      this.firstNameControl, this.middleNameControl, this.lastNameControl,
      this.titleControl, this.departmentControl, this.address1Control,
      this.address2Control, this.zipCodeControl, this.telephoneControl,
      this.dobControl
    ];
  }

  ngOnInit() {
    this.departmentService.getDepartments().subscribe(d => this.departments = d);
  }

  addTeacher(): void {
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
        this.teacherService.addTeacher(this.teacher).subscribe(
      result => {
        this.openSnackBar(`${result.firstName} ${result.lastName} added successfully!`, 'Dismiss');
      },
      err => {
        this.openSnackBar(`${this.teacher.firstName} ${this.teacher.lastName} failed to be added`, 'Dismiss');
      });
    }
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

}
