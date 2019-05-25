import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-groups',
  templateUrl: './groupCreator.component.html',
  styleUrls: ['./groupCreator.component.css']
})
export class GroupCreatorComponent implements OnInit {

  private token: any;
  private UserGroupArray = [];
  private iterator = 0;

  constructor(private http: HttpClient) {

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

  deleteUserGroup(httpOptions): void {
        this.http.delete('http://localhost:62333/groups/delete', httpOptions).subscribe(result => {

    }, error => console.error(error));
  }

  createUserGroup(httpOptions): void {
    this.http.post('http://localhost:62333/groups/create', httpOptions).subscribe(result => {

    }, error => console.error(error));
  }
}
