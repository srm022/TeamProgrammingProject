import { ValidationPatterns } from './../models/validation.patterns';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-group-edit',
  templateUrl: './group-edit.component.html',
  styleUrls: ['./group-edit.component.css']
})
export class GroupEditComponent implements OnInit {
  selectedGroupId: number;
  iterator = 0;
  groupInfoArray = [];
  token: any;
  name: any;
  description: any;
  isLoading = false;
  patterns = new ValidationPatterns();
  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef
  ) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.selectedGroupId = params['id'];
    });
    this.token = localStorage.getItem('id_token');
    this.displayGroupInfo(this.selectedGroupId);
  }

  GroupInfoToArray(result: Object | { [x: string]: any }[]) {
    while (result[this.iterator]) {
      if (result[this.iterator]['id'] == this.selectedGroupId) {
        this.name = result[this.iterator]['name'];
        this.description = result[this.iterator]['description'];
        this.groupInfoArray.push({
          GroupId: result[this.iterator]['id'],
          GroupName: result[this.iterator]['name'],
          GroupDescription: result[this.iterator]['description']
        });
        // console.log(this.description);
        // console.log(this.name);
      }

      this.iterator++;
    }
  }

  displayGroupInfo(selectedGroupId: any) {
    this.isLoading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token
      })
    };
    this.http
      .get('https://pzproject.azurewebsites.net/groups', httpOptions)
      .subscribe(
        result => {
          this.GroupInfoToArray(result);
          this.isLoading = false;
        },
        error => this.errorMsg(error)
      );
  }

  errorMsg(error: any) {
    this.isLoading = false;
    this.toastr.error('Error');
  }

  updateUserGroup(groupId: any) {
    this.isLoading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.token
      })
    };

    const bodyOptions = {
      GroupId: groupId,
      GroupName: this.name,
      GroupDescription: this.description
    };
    this.http
      .put(
        'https://pzproject.azurewebsites.net/groups/edit',
        bodyOptions,
        httpOptions
      )
      .subscribe(
        result => {
          this.showSuccessAlert();
          setTimeout(() => {
            this.isLoading = false;
            this.router.navigate(['/groups']);
          }, 2000);
        },
        error => {
          this.showGroupNameErrorAlert(error);
        }
      );
  }

  showSuccessAlert() {
    this.toastr.success('Group updated successfully');
  }

  showGroupNameErrorAlert(error: any) {
    console.error(error);
    this.isLoading = false;
    this.toastr.error('Error');
  }
}
