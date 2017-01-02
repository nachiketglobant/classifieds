import { Component } from '@angular/core';
import { AppState } from '../../app.service';
import {SettingsService} from '../../_common/services/setting.service';

let styles = require('../styles/home.component.scss').toString();
let tpls = require('../tpls/home.component.html').toString();

@Component({
  selector: 'home',
  styles : [ styles ],
  providers:[SettingsService],
  template : tpls
})

export class HomeComponent {
  private settings : any ;
  private baseUrl : any ;
  public intialCardData: any;
  constructor(public appState: AppState,private _settingsService: SettingsService) {
  }

  ngOnInit() {
    this.baseUrl=this._settingsService.getBaseUrl();
    this.intialCardData =this._settingsService.getInitialCards();
    console.log("this.intialCardData",this.intialCardData);
  }

  submitState(value: string) {
    console.log('submitState', value);
    this.appState.set('value', value);
  }
}
