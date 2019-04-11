import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private token: any;
  private email: string;
  private password: string;

  constructor(private http: HttpClient) {

  }

  ngOnInit() {
  }

  public login() {

    let credentials = {
      email: this.email,
      password: this.password,
    }

      this.http.post('https://localhost:5001/auth/login', credentials, { responseType: 'json' }).subscribe(result => {
      console.log(result)
      this.token = result['token'];
      console.log(this.token)
    }, error => console.error(error));
  }
}