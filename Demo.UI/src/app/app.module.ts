import '../polyfills';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // <-- NgModel lives here
import { HttpClientModule } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { MatIconRegistry, MatIconModule, MatButtonModule, MatInputModule, MatNativeDateModule, 
         MatTabsModule, MatExpansionModule, MatFormFieldModule, MatDatepickerModule, MatToolbarModule,
         MatGridListModule, MatListModule, MatCardModule, MatTableModule, MatPaginatorModule, 
         MatSortModule, MatProgressSpinnerModule, MatSnackBarModule, MatAutocompleteModule,
         MatSelectModule, MatDialogModule } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
// import { ChartsModule } from 'ng2-charts';

import { AppComponent } from './app.component';
import { StudentComponent } from './student/student.component';

import { StudentService } from './services/student.service';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppRoutingModule } from './app-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { scaleLinear } from "d3-scale";

import { DashboardComponent } from './dashboard/dashboard.component';
import { StudentsComponent } from './students/students.component';
import { StudentInfoComponent } from './student-info/student-info.component';
import { StudentInputComponent } from './student-input/student-input.component';
import { StudentSearchComponent } from './student-search/student-search.component';
import { MessagesComponent } from './messages/messages.component';
import { MessageService } from './services/message.service';
import { DashboardService } from './services/dashboard.service';
import { TeachersComponent } from './teachers/teachers.component';
import { TeacherService } from './services/teacher.service';
import { TeacherInputComponent } from './teacher-input/teacher-input.component';
import { DepartmentSelectComponent } from './department-select/department-select.component';
import { DepartmentService } from './services/department.service';


@NgModule({
  declarations: [
    AppComponent,
    StudentComponent,
    NavMenuComponent,
    DashboardComponent,
    StudentsComponent,
    StudentInfoComponent,
    StudentInputComponent,
    StudentSearchComponent,
    MessagesComponent,
    TeachersComponent,
    TeacherInputComponent,
    DepartmentSelectComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    MatTabsModule,
    MatIconModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatButtonModule,
    MatExpansionModule,
    MatToolbarModule,
    MatGridListModule,
    MatListModule,
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatDialogModule,
    NgxChartsModule
  ],
  providers: [ StudentService, MessageService, DashboardService, TeacherService, DepartmentService ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  constructor(matIconRegistry: MatIconRegistry, domSanitizer: DomSanitizer){
    //matIconRegistry.addSvgIconSet(domSanitizer.bypassSecurityTrustResourceUrl('./assets/mdi.svg')); // Or whatever path you placed mdi.svg at
  }
}


platformBrowserDynamic().bootstrapModule(AppModule);