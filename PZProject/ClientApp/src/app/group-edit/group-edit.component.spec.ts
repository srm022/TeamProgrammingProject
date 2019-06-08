import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ValidationPatterns } from './../models/validation.patterns';
import { Component, OnInit, ViewContainerRef, NgModule } from '@angular/core';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { GroupEditComponent } from './group-edit.component';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'; 
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientModule } from '@angular/common/http';
import { ToastModule } from 'ng2-toastr/ng2-toastr';

describe('GroupEditComponent', () => {
  let component: GroupEditComponent;
  let fixture: ComponentFixture<GroupEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        RouterTestingModule,
        MatProgressSpinnerModule,
        HttpClientModule,
        ToastModule.forRoot()
      ],
      declarations: [ GroupEditComponent ],
      providers: [ToastsManager]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
