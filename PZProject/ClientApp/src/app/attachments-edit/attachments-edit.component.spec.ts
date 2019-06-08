import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AttachmentsEditComponent } from './attachments-edit.component';

describe('AttachmentsEditComponent', () => {
  let component: AttachmentsEditComponent;
  let fixture: ComponentFixture<AttachmentsEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AttachmentsEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AttachmentsEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
