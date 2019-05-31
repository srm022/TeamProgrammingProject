import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';


@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  private token: any;
  private first = [];
  private second = [];
  private iterator = 0;
  UserGroupArray = [];
  private isLoading = true;
  constructor(
    private http: HttpClient,
    private router: Router,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef) {
      this.toastr.setRootViewContainerRef(vcr);
    }

  ngOnInit() {
    this.token = localStorage.getItem('id_token');
    this.displayUserGroups();

  }

  addUserGroupstoArray(result: Object | { [x: string]: any; }[]) {
    while (result[this.iterator]) {

      this.UserGroupArray.push({
        GroupId: result[this.iterator]['groupId'],
        GroupName: result[this.iterator]['name'],
        UserId: result[this.iterator]['creatorId'],
        description: result[this.iterator]['description'],
        userGroups: result[this.iterator]['userGroups']
      });

      this.iterator++;
    }
  }

  displayUserGroups() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    }
    this.http.get('https://pzproject.azurewebsites.net/groups', httpOptions).subscribe(result => {
      this.addUserGroupstoArray(result);
      console.log(result);
      this.isLoading = false;
    }, error => console.error(error));

  }

  deleteUserGroup(groupId: any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.token
      }),
      body: {
        'GroupId': groupId
      },
    };

    this.http.delete('https://pzproject.azurewebsites.net/groups/delete', httpOptions).subscribe(result => {
      window.location.reload();
    }, error => { this.showErrorDeletingGroup(error); });
  }

  showErrorDeletingGroup(error: any) {
    console.error(error);
    this.toastr.error('Cannot delete group. Admin status required.');
  }

  showSuccesAsign() {
    this.toastr.success('User assinged');
  }

  showErrorAdd(error: any) {
    console.error(error);
    this.toastr.error('Email adress must be valid, Admin status required, Given adress might already be in the group', 'Error:');
  }

  showSuccesRemoved() {
    this.toastr.success('User remove from group');
  }

  showErrorRem(error: any) {
    console.error(error);
    this.toastr.error('User ID must be valid, Admin status required', 'Error:');
  }

  showUserGroupList(groupId: any) {
    
    this.router.navigate(['/group-users-display'], { queryParams: {groupId: groupId}});
}
}
