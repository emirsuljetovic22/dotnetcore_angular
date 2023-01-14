import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditJobCategoryComponent } from './edit-job-category.component';

describe('EditJobCategoryComponent', () => {
  let component: EditJobCategoryComponent;
  let fixture: ComponentFixture<EditJobCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditJobCategoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditJobCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
