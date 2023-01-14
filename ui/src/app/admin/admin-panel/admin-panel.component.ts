import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  articlesTab = false;
  infoTab = false;
  jobsTab = false;

  tabChange(index: number){
    if(index === 1){
      this.articlesTab = true;
    } else if (index === 2) {
      this.infoTab = true;
    } else if (index === 3) {
      this.jobsTab = true;
    }
  }

}
