import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-group-users-display',
  templateUrl: './group-users-display.component.html',
  styleUrls: ['./group-users-display.component.css']
})
export class GroupUsersDisplayComponent implements OnInit {

  token: any;
  iterator = 0;
  userGroupArray = [];
  selectedGroupId;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.selectedGroupId = params["groupId"];
    });
    this.token = localStorage.getItem('id_token');
    this.displayGroupNotes(this.selectedGroupId);
    console.log(this.selectedGroupId);
  }

  GroupNotestoArray(result: Object | { [x: string]: any; }[]) {

    while (result[this.iterator]) {

      if (result[this.iterator]['groupId'] == this.selectedGroupId) {
        this.userGroupArray.push({
          GroupId: result[this.iterator]['groupId'],
          GroupName: result[this.iterator]['name'],
          userGroups: result[this.iterator]['userGroups']
        });
      }


      this.iterator++;
    }
  }

  displayGroupNotes(selectedGroupId: any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    }
    this.http.get('https://pzproject.azurewebsites.net/groups', httpOptions).subscribe(result => {
      this.GroupNotestoArray(result);

    }, error => console.error(error));

  }

  removeUserFromGroup(userId: any, groupId: any) {
    this.token = localStorage.getItem('id_token');

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.token
      })
    };

    const bodyOptions = {
      'UserId': userId,
      'GroupId': groupId
    }
    this.http.post('https://pzproject.azurewebsites.net/groups/remove', bodyOptions, httpOptions).subscribe(result => {
      window.location.reload();
    }, error => { this.showErrorRem(error); });
  }

  assignUserGroup(userEmail: any, groupName: any) {
    this.token = localStorage.getItem('id_token');

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.token
      })
    };

    const bodyOptions = {
      'UserEmail': userEmail,
      'GroupName': groupName
    };

    this.http.post('https://pzproject.azurewebsites.net/groups/assign', bodyOptions, httpOptions).subscribe(result => {
      window.location.reload();
      this.showSuccesAsign();
    }, error => { this.showErrorAdd(error); });
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
}

