import { TestBed, inject } from '@angular/core/testing';

import { DepartmentService } from './department.service';

describe('DepartmentServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DepartmentService]
    });
  });

  it('should be created', inject([DepartmentService], (service: DepartmentService) => {
    expect(service).toBeTruthy();
  }));
});
