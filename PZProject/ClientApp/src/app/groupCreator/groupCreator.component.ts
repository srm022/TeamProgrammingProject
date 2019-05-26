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
  private Name: string;
  private CreatorId: string;

  constructor(private http: HttpClient) {

  }

  ngOnInit() {

  }

  createUserGroup(GroupName: String) {
    this.token = localStorage.getItem('id_token');

    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.token
      })
    };

    let bodyOptions = {
      "GroupName": GroupName
    }
    this.http.post('http://localhost:62333/groups/create', bodyOptions, httpOptions).subscribe(result => {

    }, error => { console.error(error); });
  }
}
