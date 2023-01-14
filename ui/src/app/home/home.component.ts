import { Component, OnInit } from '@angular/core';
import { LoaderService } from '../_services/loader.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  loaderService: false;

  constructor(loaderService: LoaderService) {}

  ngOnInit(): void {}


  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
