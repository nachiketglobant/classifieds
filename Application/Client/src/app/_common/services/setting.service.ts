import {Injectable} from '@angular/core';

var settingsJson = require("app/settings.json");
var bannerListingsJson = require("app/banner/json/banner.json");
var cardListingsJson = require("app/card-list/json/card-list.json");

@Injectable()
export class SettingsService{
  public  settings : any ;

  constructor() {
  }

  getSettings(){
    return settingsJson;
  }

  getBaseUrl(){
    return settingsJson.services.main;
  }

  getBannerListingsData(){
      return bannerListingsJson.details;
  }

  getCardListingsData(){
      return cardListingsJson.categories;
  }

}



