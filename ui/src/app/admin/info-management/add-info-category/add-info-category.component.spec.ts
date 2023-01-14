import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInfoCategoryComponent } from './add-info-category.component';

describe('AddInfoCategoryComponent', () => {
  let component: AddInfoCategoryComponent;
  let fixture: ComponentFixture<AddInfoCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddInfoCategoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddInfoCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
