import { Component, OnInit, Inject, ViewContainerRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userLogin: string;
  email: string;
  password: string;
  isLoading = false;
  constructor(
    private http: HttpClient,
    private router: Router,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.userLogin = localStorage.getItem('email');
  }

  public login() {
    this.isLoading = true;
    const credentials = {
      email: this.email,
      password: this.password
    };

    this.http
      .post('https://pzproject.azurewebsites.net/auth/login', credentials, {
        responseType: 'json'
      })
      .subscribe(
        result => {
          this.isLoading = false;
          localStorage.setItem('id_token', result['token']);
          localStorage.setItem('userId', result['userId']);
          localStorage.setItem('email', this.email);
          this.router.navigate(['/']);
        },
        error => {
          console.error(error);
          this.isLoading = false;
          this.showLoginError();
        }
      );
  }

  showLoginError() {
    this.toastr.error('Invalid login or password!');
  }

  isTokenPresent(): boolean {
    return localStorage.getItem('id_token') ? true : false;
  }

  clearStorage() {
    localStorage.clear();
  }


}
