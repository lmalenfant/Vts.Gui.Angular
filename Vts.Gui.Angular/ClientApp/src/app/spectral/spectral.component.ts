import { Component } from '@angular/core';
import { AbsorberConcentration } from '../tissue-definition/absorber-concentration.model';
import { Skin } from '../tissue-definition/absorber-list';
import { BloodConcentration } from '../tissue-definition/blood-concentration.model';
import { Range } from '../range/range.model';
import { ListType } from '../shared/list-definition.model';

@Component({
    selector: 'app-spectral',
    templateUrl: './spectral.component.html',
    styleUrls: ['./spectral.component.css']
})
/** spectral component*/
export class SpectralComponent {
  scattererType: ListType = { value: 'PowerLaw', display: 'PowerLaw [A*λ^(-b)]' };
  tissueType: ListType = { value: 'Skin', display: 'Skin' };
  absorberConcentration: Array<AbsorberConcentration> = Skin;
  bloodConcentration: BloodConcentration = { totalHb: 80, bloodVolume: 0.0344, stO2: 0.7, visible: true };
  range: Range = {
    title: 'Wavelength Range',
    startLabel: 'Begin',
    startLabelUnits: 'nm',
    startValue: 650,
    endLabel: 'End',
    endLabelUnits: 'nm',
    endValue: 1000,
    numberLabel: 'Number',
    numberValue: 36
  };

  constructor() {

    }
}
