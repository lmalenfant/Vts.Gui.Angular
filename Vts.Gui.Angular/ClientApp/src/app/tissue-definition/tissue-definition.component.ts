import { Component, Input } from '@angular/core';
import { BrainGrayMatter, BrainWhiteMatter, BreastPostMenopause, BreastPreMenopause, Custom, IntralipidPhantom, Liver, Skin } from '../tissue-definition/absorber-list';
import { AbsorberConcentration } from './absorber-concentration.model';
import { BloodConcentration } from './blood-concentration.model';
import { TissueType } from './tissue-definition.model';
import { TissueTypeList } from './tissue-list';

@Component({
    selector: 'app-tissue-definition',
    templateUrl: './tissue-definition.component.html',
    styleUrls: ['./tissue-definition.component.css']
})
/** tissue-definition component*/
export class TissueDefinitionComponent {
  @Input() tissueType: TissueType;
  @Input('tissueTypeList') tissueTypeList = TissueTypeList;
  @Input() bloodConcentration: BloodConcentration;
  @Input('absorberConcentration') absorberConcentration: Array<AbsorberConcentration>;

  constructor() {

  }
  onChange(value) {
    console.log(this.tissueType.value);
    console.log(value);
    this.tissueType.value = value;
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
        break;
      case 'Custom':
        this.absorberConcentration = Custom;
        break;
    }
  }
}
