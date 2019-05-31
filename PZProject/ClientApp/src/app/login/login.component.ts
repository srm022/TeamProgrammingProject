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
  private email: string;
  private password: string;
  private isLoading = false;
  constructor(
    private http: HttpClient, 
    private router: Router, 
    private toastr: ToastsManager,
    private vcr: ViewContainerRef) {
      this.toastr.setRootViewContainerRef(vcr);
    }

  ngOnInit() {}

  public login() {
    this.isLoading = true;
    let credentials = {
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
    localStorage.removeItem('id_token');
  }
}
