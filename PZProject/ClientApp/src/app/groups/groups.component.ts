import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders} from '@angular/common/http';


@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  private token: any;
  private UserGroupArray = [];
  private iterator = 0;

  constructor(private http: HttpClient) {
 
  }

  ngOnInit() {
    this.displayUserGroups();
  }

  displayUserGroups():void {
  this.token = localStorage.getItem('id_token');

  let httpOptions = {
    headers: new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    })
  }

  this.http.get('http://localhost:62333/groups', httpOptions).subscribe(result => {
    console.log(result);

    while (result[this.iterator]) {

      this.UserGroupArray.push({
        GroupId: result[this.iterator]['groupId'],
        GroupName: result[this.iterator]['name'],
        UserId: result[this.iterator]['creatorId']
      });

      this.iterator++;
    }

  }, error => console.error(error));
}
}
