import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoteEditComponentComponent } from './note-edit-component.component';

describe('NoteEditComponentComponent', () => {
  let component: NoteEditComponentComponent;
  let fixture: ComponentFixture<NoteEditComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoteEditComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoteEditComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
