import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupUsersDisplayComponent } from './group-users-display.component';
import { ValidationPatterns } from './../models/validation.patterns';
import { Component, OnInit, ViewContainerRef, NgModule } from '@angular/core';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'; 
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import {MatListModule} from '@angular/material/list';

describe('GroupUsersDisplayComponent', () => {
  let component: GroupUsersDisplayComponent;
  let fixture: ComponentFixture<GroupUsersDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        RouterTestingModule,
        MatProgressSpinnerModule,
        HttpClientModule,
        ToastModule.forRoot(),
        MatListModule 
      ],
      declarations: [ GroupUsersDisplayComponent ],
      providers: [ToastsManager]
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
