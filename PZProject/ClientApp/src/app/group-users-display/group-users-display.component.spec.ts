import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupUsersDisplayComponent } from './group-users-display.component';

describe('GroupUsersDisplayComponent', () => {
  let component: GroupUsersDisplayComponent;
  let fixture: ComponentFixture<GroupUsersDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupUsersDisplayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupUsersDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
