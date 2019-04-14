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
  constructor(private http: HttpClient) {
    this.zmienna = localStorage.getItem('id_token');

    let httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.zmienna
      })
    }
    console.log(this.zmienna);
    this.http.get('http://localhost:62333/groups', httpOptions).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
