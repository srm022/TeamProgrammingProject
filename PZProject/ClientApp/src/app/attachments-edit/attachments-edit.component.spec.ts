import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AttachmentsEditComponent } from './attachments-edit.component';
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
import { MatCardModule } from '@angular/material/card';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatExpansionModule} from '@angular/material/expansion';

describe('AttachmentsEditComponent', () => {
  let component: AttachmentsEditComponent;
  let fixture: ComponentFixture<AttachmentsEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule,
        RouterTestingModule,
        MatProgressSpinnerModule,
        HttpClientModule,
        ToastModule.forRoot(),
        MatListModule,
        MatCardModule,
        MatGridListModule,
        MatExpansionModule 
      ],
      declarations: [ AttachmentsEditComponent ],
      providers: [ToastsManager]
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
