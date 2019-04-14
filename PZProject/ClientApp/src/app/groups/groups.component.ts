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
  private i = 0;

  constructor(private http: HttpClient) {
    this.token = localStorage.getItem('id_token');

    let httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    }

    this.http.get('http://localhost:62333/groups', httpOptions).subscribe(result => {
      console.log(result);

      while (result[this.i]) {

        this.UserGroupArray.push({
          GroupId: result[this.i]['groupId'],
          GroupName: result[this.i]['name'],
          UserId: result[this.i]['creatorId']
        });

        this.i++;
      }

    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
