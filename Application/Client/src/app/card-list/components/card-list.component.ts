import { Component } from '@angular/core';
import { AppState } from '../../app.service';
import { SettingsService } from '../../_common/services/setting.service';

let styles = require('../styles/card-list.component.scss').toString();
let tpls = require('../tpls/card-list.component.html').toString();

@Component({
    selector : 'card-list',
    styles : [styles],
    providers:[SettingsService],
    template : tpls
})
export class CardListComponent{
    private cardListings : any;
    constructor(public appState: AppState,private _settingsService : SettingsService){

    }

    ngOnInit(){
        this.cardListings = this._settingsService.getCardListingsData();

    }
}