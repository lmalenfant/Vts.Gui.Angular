import { Component, Input, OnInit } from '@angular/core';
import { ListType } from '../shared/list-definition.model';
import { BrainGrayMatter, BrainWhiteMatter, BreastPostMenopause, BreastPreMenopause, Custom, IntralipidPhantom, Liver, Skin } from '../tissue-definition/absorber-list';
import { AbsorberConcentration } from './absorber-concentration.model';
import { BloodConcentration } from './blood-concentration.model';
import { TissueTypeList } from './tissue-list';
import { ScattererTypeList } from '../scatterer-type/scatterer-list';
import { PowerLawScatterer as PowerLaw } from '../scatterer-type/power-law.model';
import { IntralipidScatterer as Intralipid } from '../scatterer-type/intralipid.model';
import { MieScatterer as MieParticle } from '../scatterer-type/mie-particle.model';
import * as $ from 'jquery';

@Component({
    selector: 'app-tissue-definition',
    templateUrl: './tissue-definition.component.html',
    styleUrls: ['./tissue-definition.component.css']
})
/** tissue-definition component*/
export class TissueDefinitionComponent {
  @Input() tissueType: ListType;
  @Input('tissueTypeList') tissueTypeList = TissueTypeList;
  @Input() bloodConcentration: BloodConcentration;
  @Input('absorberConcentration') absorberConcentration: Array<AbsorberConcentration>;
  @Input() scattererType: ListType;
  @Input('scattererTypeList') scattererTypeList = ScattererTypeList;
  @Input() powerLaw: PowerLaw;
  @Input() intralipid: Intralipid;
  @Input() mieParticle: MieParticle;

  constructor() {
  }

  ngOnInit() {
    this.onChange(this.tissueType.value);
  }

  calculateBloodConcentration() {
    let Hb = 0;
    let HbO2 = 0;
    this.absorberConcentration.forEach(function (val) {
      console.log("label = " + val.label + " value = " + val.value);
      if (val.label === "Hb") {
        Hb = val.value;
      }
      if (val.label === "HbO2") {
        HbO2 = val.value;
      }
    });
    this.bloodConcentration.totalHb = Hb + HbO2;
    this.bloodConcentration.stO2 = HbO2 / (Hb + HbO2);
    this.bloodConcentration.bloodVolume = this.bloodConcentration.totalHb / 1E6 * 64500 / 150;
  }

  onChange(value) {
    console.log("Display: " + this.tissueType.display + " Value: " + this.tissueType.value);
    console.log(value);
    this.tissueType.value = value;
    this.bloodConcentration.visible = true;
    $('#scatterer-type').val('PowerLaw');
    this.scattererType.value = 'PowerLaw';
    this.scattererType.display = 'PowerLaw [A*λ^(-b)]';
    this.powerLaw.show = true;
    this.intralipid.show = false;
    this.mieParticle.show = false;
    switch (value) {
      case 'Skin':
        this.absorberConcentration = Skin;
        this.powerLaw.a = 1.2;
        this.powerLaw.b = 1.42;
        break;
      case 'Liver':
        this.absorberConcentration = Liver;
        this.powerLaw.a = 0.84;
        this.powerLaw.b = 0.55;
        break;
      case 'BrainGrayMatter':
        this.absorberConcentration = BrainGrayMatter;
        this.powerLaw.a = 0.56;
        this.powerLaw.b = 1.36;
        break;
      case 'BrainWhiteMatter':
        this.absorberConcentration = BrainWhiteMatter;
        this.powerLaw.a = 3.56;
        this.powerLaw.b = 0.84;
        break;
      case 'BreastPreMenopause':
        this.absorberConcentration = BreastPreMenopause;
        this.powerLaw.a = 0.67;
        this.powerLaw.b = 0.95;
        break;
      case 'BreastPostMenopause':
        this.absorberConcentration = BreastPostMenopause;
        this.powerLaw.a = 0.72;
        this.powerLaw.b = 0.58;
        break;
      case 'IntralipidPhantom':
        this.absorberConcentration = IntralipidPhantom;
        this.bloodConcentration.visible = false;
        $('#scatterer-type').val('Intralipid');
        this.scattererType.value = 'Intralipid';
        this.scattererType.display = 'Intralipid [vol. frac.]';
        this.powerLaw.show = false;
        this.intralipid.show = true;
        break;
      case 'Custom':
        this.absorberConcentration = Custom;
        this.powerLaw.a = 1;
        this.powerLaw.b = 0.1;
        break;
    }
    if (this.absorberConcentration !== IntralipidPhantom) {
      this.calculateBloodConcentration();
    }
  }
}
