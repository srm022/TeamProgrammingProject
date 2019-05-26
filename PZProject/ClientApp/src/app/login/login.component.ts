import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private successfulLogin: boolean;
  private email: string;
  private password: string;

  constructor(
    private http: HttpClient,
    private router: Router) {

  }

  ngOnInit() {
  }

  public login() {
    
    let credentials = {
      email: this.email,
      password: this.password,
    }

    this.http.post('http://localhost:62333/auth/login', credentials, { responseType: 'json' }).subscribe(result => {
      localStorage.setItem('id_token', result['token']);
      this.successfulLogin = true;
      this.router.navigate(['/']);
    }, error =>{ console.error(error); this.successfulLogin = false; });
    

  }
  
   isTokenPresent(): boolean {
    return localStorage.getItem("id_token") ? true : false;
  }

   clearStorage() {
    localStorage.removeItem("id_token");
    this.successfulLogin = true;
  }
}
