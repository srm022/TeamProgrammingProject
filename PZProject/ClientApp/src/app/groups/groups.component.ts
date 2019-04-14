import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders} from '@angular/common/http';


@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  private zmienna: any;
  private GroupNameArray = [];
  private GroupIdArray = [];
  private UserIdArray = [];
  private i = 0;

  constructor(private http: HttpClient) {
    this.zmienna = localStorage.getItem('id_token');

    let httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.zmienna
      })
    }

    this.http.get('http://localhost:62333/groups', httpOptions).subscribe(result => {
      console.log(result);

      while (result[this.i]) {
        this.GroupIdArray.push(result[0]['groupId']);
        this.GroupNameArray.push(result[0]['name']);
        this.UserIdArray.push(result[0]['creatorId']);
        this.GroupIdArray.push(result[1]['groupId']);
        this.GroupNameArray.push(result[1]['name']);
        this.UserIdArray.push(result[1]['creatorId']);
        console.log(this.GroupIdArray[this.i]);
        console.log(this.GroupNameArray[this.i]);
        console.log(this.UserIdArray[this.i]);
        this.i++;
      }



    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
