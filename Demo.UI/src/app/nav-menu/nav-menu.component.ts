import { Component, OnInit } from '@angular/core';

import { DashboardComponent } from '../dashboard/dashboard.component';
import { StudentsComponent } from '../students/students.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
