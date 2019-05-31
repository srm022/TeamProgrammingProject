import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';


@Component({
  selector: 'app-groups',
  templateUrl: './groupCreator.component.html',
  styleUrls: ['./groupCreator.component.css']
})
export class GroupCreatorComponent implements OnInit {

  token: any;
  groupName: string;
  groupDescription: string;

  isLoading = false;
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

  createUserGroup() {
    this.isLoading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.token
      })
    };

    const bodyOptions = {
      'GroupName': this.groupName,
      'GroupDescription': this.groupDescription
    };
    this.http.post('https://pzproject.azurewebsites.net/groups/create', bodyOptions, httpOptions).subscribe(result => {
    this.showSuccessAlert();
      setTimeout(() => {
        this.isLoading = false;
        this.router.navigate(['/groups']);
      },
      2000);
    }, error => { this.showGroupNameErrorAlert(error); }
    );
  }

  showSuccessAlert() {
    this.toastr.success('Group "' + this.groupName + '" created successfully');
  }

  showGroupNameErrorAlert(error: any) {
    console.error(error);
    this.isLoading = false;
    this.toastr.error('Group name already taken');
  }
}
