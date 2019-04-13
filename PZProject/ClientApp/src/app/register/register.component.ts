import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  private email: string;
  private password: string;
  private firstName: string;
  private lastName: string;

  constructor(private http: HttpClient) {

  }

  ngOnInit() {
  }

  public register() {

    let credentials = {
        email: this.email,
        password:  this.password,
        firstName: this.firstName,
        lastName: this.lastName,
    }
    this.http.post('https://localhost:44366/auth/register', credentials, { responseType: 'text' }).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }

}
