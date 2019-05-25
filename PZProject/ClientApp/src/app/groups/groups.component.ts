import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  private token: any;
  private UserGroupArray = [];
  private iterator = 0;

  constructor(
    private http: HttpClient,
    private router: Router) {

  }

  ngOnInit() {
    this.token = localStorage.getItem('id_token');

    let httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    }

    this.displayUserGroups(httpOptions);

  }

  addUserGroupstoArray(result) : void {
    while (result[this.iterator]) {

      this.UserGroupArray.push({
        GroupId: result[this.iterator]['groupId'],
        GroupName: result[this.iterator]['name'],
        UserId: result[this.iterator]['creatorId']
      });

      this.iterator++;
    }
  }

  displayUserGroups(httpOptions): void {

    this.http.get('http://localhost:62333/groups', httpOptions).subscribe(result => {
      this.addUserGroupstoArray(result);
      console.log(result);

      }, error => console.error(error));

  }

  deleteUserGroup(GroupId) {
    this.token = localStorage.getItem('id_token');

    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.token
      }),
      body: {
        "GroupId": GroupId
      },
    };

    console.log(GroupId);

    this.http.delete('http://localhost:62333/groups/delete', httpOptions).subscribe(result => {
      window.location.reload;
    }, error => { console.error(error); });
  }


}
