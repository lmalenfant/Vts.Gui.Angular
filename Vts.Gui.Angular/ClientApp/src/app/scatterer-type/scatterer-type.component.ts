import { Component, Input } from '@angular/core';
import { ListType } from '../shared/list-definition.model';
import { ScattererTypeList } from './scatterer-list';

@Component({
    selector: 'app-scatterer-type',
    templateUrl: './scatterer-type.component.html',
    styleUrls: ['./scatterer-type.component.css']
})
/** scatterer-type component*/
export class ScattererTypeComponent {
  title: string = "Scatterer Type";
  @Input() scattererType: ListType;
  @Input('scattererTypeList') scattererTypeList = ScattererTypeList;

  constructor() {

  }

  onChange(value) {
    console.log(this.scattererType.value);
    console.log(value);
  }
}
