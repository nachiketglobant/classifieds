import {Injectable} from '@angular/core';
import { Observable }     from 'rxjs/Observable';
import { Http, Response,RequestOptions } from '@angular/http';
import {CService} from  './http.service';
import 'rxjs/Rx';

var settingsJson = require("app/settings.json");

var bannerListingsJson = require("app/banner/json/banner.json");
var cardListingsJson = require("app/card-list/json/card-list.json");


@Injectable()
export class SettingsService{
  public  settings : any ;
  private cardUrl = 'http://in-it0289/ListingAPI/api/Listings/GetTopListings';

  constructor(private _cservice:CService) {
  }

  getSettings(){
    return settingsJson;
  }

  getBaseUrl(){
    return settingsJson.services.main;
  }
  getInitialCards (){
      console.log('in side get',this.cardUrl);
      return this._cservice.observableGetHttp(this.cardUrl,null,false,)
         .subscribe((res:Response)=> {console.log("res :",res);},
        error => {
            console.log("error in response");
        },
        ()=>{
          console.log("Finally");
        })
    }
getBannerListingsData(){
      return bannerListingsJson.details;
  }

  getCardListingsData(){
      return cardListingsJson.categories;
  }

}



