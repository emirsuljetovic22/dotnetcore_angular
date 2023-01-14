import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { AccountService } from '../_services/account.service';
import { LoaderService } from '../_services/loader.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(public accountService: AccountService, private router: Router, public loaderService: LoaderService, public dialog: MatDialog) { }

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/home');
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  openRegisterForm() {
    console.log("triggered");
    this.dialog.open(RegisterComponent);
  }

  openLoginForm() {
    this.dialog.open(LoginComponent);
  }

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(RegisterComponent, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }

}
