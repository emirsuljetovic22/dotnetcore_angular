import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditInfoCategoryComponent } from './edit-info-category.component';

describe('EditInfoCategoryComponent', () => {
  let component: EditInfoCategoryComponent;
  let fixture: ComponentFixture<EditInfoCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditInfoCategoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditInfoCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
