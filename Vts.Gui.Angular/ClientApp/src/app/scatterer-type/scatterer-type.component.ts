import { Component, Input } from '@angular/core';
import { ListType } from '../shared/list-definition.model';
import { ScattererTypeList } from './scatterer-list';
import { PowerLawScatterer as PowerLaw } from './power-law.model';
import { IntralipidScatterer as Intralipid } from './intralipid.model';
import { MieScatterer as MieParticle } from './mie-particle.model';

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
  @Input() powerLaw: PowerLaw;
  @Input() intralipid: Intralipid;
  @Input() mieParticle: MieParticle;

  constructor() {

  }

  onChange(value) {
    console.log("Current Scatterer Type: " + this.scattererType.value);
    console.log("New Scatterer Type: " + value);
    this.scattererType.value = value;
    switch (value) {
      case 'PowerLaw':
        this.powerLaw.show = true;
        this.intralipid.show = false;
        this.mieParticle.show = false;
        break;
      case 'Intralipid':
        this.powerLaw.show = false;
        this.intralipid.show = true;
        this.mieParticle.show = false;
        break;
      case 'Mie':
        this.powerLaw.show = false;
        this.intralipid.show = false;
        this.mieParticle.show = true;
        break;
    }
  }
}
