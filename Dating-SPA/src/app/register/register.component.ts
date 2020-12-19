import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  // @Input() valuesFromHome:any;
  @Output() cancelRegister: any = new EventEmitter();

  model:any = {};

  constructor(private authService : AuthService) { }

  ngOnInit() {
  }

  register() {

    this.authService.register(this.model).subscribe(() => {
      console.log('registration successful');
    }, error => {
      console.log(error);
    });
  }

cancelled() {
   this.cancelRegister.emit(false);
    console.log('Cancelled clicked');
}

}
