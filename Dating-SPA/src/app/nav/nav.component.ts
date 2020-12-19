import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model:any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login (){
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged In successfully');
        }, error => { console.log('Failed to logged in');
      });
  }

  loggedIn() {
     const token = localStorage.getItem('token');
     return !!token;
  }

  loggedOut() {
    localStorage.removeItem('token');
    console.log('Logged out');
 }




}
