import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { HttpClient } from 'selenium-webdriver/http';
import { Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-group-users-display',
  templateUrl: './group-users-display.component.html',
  styleUrls: ['./group-users-display.component.css']
})
export class GroupUsersDisplayComponent implements OnInit {

  token: any;
  private iterator = 0;
  private GroupNotesArray = [];

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef) {
      this.toastr.setRootViewContainerRef(vcr);
    }

  ngOnInit() {
    this.token = localStorage.getItem('id_token');
  }

  addUserGroupstoArray(result: Object | { [x: string]: any; }[]) {

    while (result[this.iterator]) {

      this.GroupNotesArray.push({
        GroupId: result[this.iterator]['groupId'],
        GroupName: result[this.iterator]['name'],
        UserId: result[this.iterator]['creatorId'],
        description: result[this.iterator]['description']
      });

      this.iterator++;
    }
  }
  
  displayGroupNotes() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    }
    this.http.get('https://pzproject.azurewebsites.net/groups', httpOptions).subscribe(result => {
      this.addUserGroupstoArray(result);
      console.log(result);
  
    }, error => console.error(error));
  
  }

}

