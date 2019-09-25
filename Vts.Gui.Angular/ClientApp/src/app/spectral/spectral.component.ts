import { Component } from '@angular/core';
import { AbsorberConcentration } from '../tissue-definition/absorber-concentration.model';
import { Skin } from '../tissue-definition/absorber-list';
import { BloodConcentration } from '../tissue-definition/blood-concentration.model';
import { TissueType } from '../tissue-definition/tissue-definition.model';

@Component({
    selector: 'app-spectral',
    templateUrl: './spectral.component.html',
    styleUrls: ['./spectral.component.css']
})
/** spectral component*/
export class SpectralComponent {
  tissueType: TissueType = { value: 'Skin', display: 'Skin' };
  absorberConcentration: Array<AbsorberConcentration> = Skin;
  bloodConcentration: BloodConcentration = { totalHb: 80, bloodVolume: 0.0344, stO2: 0.7, visible: true };
  constructor() {

    }
}
