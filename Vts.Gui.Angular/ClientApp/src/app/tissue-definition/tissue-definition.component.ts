import { Component, Input, OnInit } from '@angular/core';
import { ListType } from '../shared/list-definition.model';
import { BrainGrayMatter, BrainWhiteMatter, BreastPostMenopause, BreastPreMenopause, Custom, IntralipidPhantom, Liver, Skin } from '../tissue-definition/absorber-list';
import { AbsorberConcentration } from './absorber-concentration.model';
import { BloodConcentration } from './blood-concentration.model';
import { TissueTypeList } from './tissue-list';

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

  constructor() {
  }

  ngOnInit() {
    this.calculateBloodConcentration();
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
    console.log(this.tissueType.value);
    console.log(value);
    this.tissueType.value = value;
    this.bloodConcentration.visible = true;
    switch (this.tissueType.value) {
      case 'Skin':
        this.absorberConcentration = Skin;
        break;
      case 'Liver':
        this.absorberConcentration = Liver;

        break;
      case 'BrainGrayMatter':
        this.absorberConcentration = BrainGrayMatter;
        break;
      case 'BrainWhiteMatter':
        this.absorberConcentration = BrainWhiteMatter;
        break;
      case 'BreastPreMenopause':
        this.absorberConcentration = BreastPreMenopause;
        break;
      case 'BreastPostMenopause':
        this.absorberConcentration = BreastPostMenopause;
        break;
      case 'IntralipidPhantom':
        this.absorberConcentration = IntralipidPhantom;
        this.bloodConcentration.visible = false;
        break;
      case 'Custom':
        this.absorberConcentration = Custom;
        break;
    }
    if (this.absorberConcentration !== IntralipidPhantom) {
      this.calculateBloodConcentration();
    }
  }
}
