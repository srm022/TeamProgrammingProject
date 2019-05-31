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
  private iterator = 0;
  private userGroupArray = [];
  private selectedGroupId;

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

      if(result[this.iterator]['groupId'] == this.selectedGroupId){
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

}

