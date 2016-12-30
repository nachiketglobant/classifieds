import { Component } from '@angular/core';
import { AppState } from '../../app.service';

let styles = require('../styles/filter.component.scss').toString();
let tpls = require('../tpls/filter.component.html').toString();

@Component({
  selector: 'filter',
  styles : [ styles ],
  template : tpls
})

export class FilterComponent {

}
