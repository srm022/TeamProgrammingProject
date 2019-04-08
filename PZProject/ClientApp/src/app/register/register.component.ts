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

    let credentials = new RegisterModel();
        credentials.email = this.email;
        credentials.password = this.password;
        credentials.firstName = this.firstName;
        credentials.lastName = this.lastName;

    this.http.post('https://localhost:5001/auth/register', credentials, { responseType: 'text' }).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }

}

class RegisterModel {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
}