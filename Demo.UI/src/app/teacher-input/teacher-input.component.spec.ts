import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherInputComponent } from './teacher-input.component';

describe('TeacherInputComponent', () => {
  let component: TeacherInputComponent;
  let fixture: ComponentFixture<TeacherInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TeacherInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TeacherInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
