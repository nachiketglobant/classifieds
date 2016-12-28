import { Component } from '@angular/core';
import { AppState } from '../../app.service';
import {SettingsService} from '../../_common/services/setting.service';

let styles = require('../styles/banner.component.scss').toString();
let tpls = require('../tpls/banner.component.html').toString();

@Component({
  selector: 'banner',
  styles : [ styles ],
  providers:[SettingsService],
  template : tpls
})

export class BannerComponent {
  private settings : any ;
  private baseUrl : any ;
  localState = { value: '' };
  constructor(public appState: AppState,private _settingsService: SettingsService) {}

  ngOnInit() {
    this.baseUrl=this._settingsService.getBaseUrl();
  }

  submitState(value: string) {
    console.log('submitState', value);
    this.appState.set('value', value);
    this.localState.value = '';
  }
}
