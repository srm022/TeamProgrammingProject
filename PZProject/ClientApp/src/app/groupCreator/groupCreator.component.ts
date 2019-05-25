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
    this.token = localStorage.getItem('id_token');

    let httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    }
  }

  createUserGroup(httpOptions): void {
    this.http.post('http://localhost:62333/groups/create', httpOptions).subscribe(result => {
      console.log(result);

    }, error => console.error(error));
  }
}
