import { Component, OnInit, ViewContainerRef } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { ToastsManager } from "ng2-toastr";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.css"]
})
export class RegisterComponent implements OnInit {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  emailPattern = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef
  ) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {}

  public register() {
      let credentials = {
        email: this.email,
        password: this.password,
        firstName: this.firstName,
        lastName: this.lastName
      };

      this.http.post('https://pzproject.azurewebsites.net/auth/register', credentials, {
          responseType: 'text'
        })
        .subscribe(
          result => {
            console.log(result);
            this.router.navigate(['/login']);
          },
          error => console.log(error)
        );
        this.showEmailTakenError()
  }

  showEmailTakenError() {
    this.toastr.error('This email adress is already taken');
  }
}
