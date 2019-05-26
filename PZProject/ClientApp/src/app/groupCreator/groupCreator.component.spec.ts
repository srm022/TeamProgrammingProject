import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupCreatorComponent } from './groupCreator.component';

describe('GroupCreatorComponent', () => {
  let component: GroupCreatorComponent;
  let fixture: ComponentFixture<GroupCreatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [GroupCreatorComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupCreatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
