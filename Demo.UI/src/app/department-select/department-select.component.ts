import { Component, OnInit } from '@angular/core';
import {FormControl} from '@angular/forms';
import { DepartmentService } from '../services/department.service';
import { Department } from '../dto/department';

@Component({
  selector: 'app-department-select',
  templateUrl: './department-select.component.html',
  styleUrls: ['./department-select.component.css']
})
export class DepartmentSelectComponent implements OnInit {
  
  myControl: FormControl = new FormControl();

  departments: Department[];

  constructor(private departmentService: DepartmentService) { }

  ngOnInit() {
    this.departmentService.getDepartments().subscribe(t => this.departments = t);
  }

}
