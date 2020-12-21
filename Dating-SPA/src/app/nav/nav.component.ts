import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  model:any = {};

  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login (){
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged In successfully');
      // console.log('Logged In successfully');
        }, error => {
          this.alertify.error(error);
        //  console.log('errorbbbbbbb');
      });
  }

  loggedIn() {
    //  const token = localStorage.getItem('token');
    //  return !!token;
    return this.authService.loggedIn();
  }

  loggedOut() {
    localStorage.removeItem('token');
    this.alertify.message('Logged out');
 }




}
