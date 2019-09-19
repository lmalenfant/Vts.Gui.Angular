import { Component } from '@angular/core';
import { TissueType } from '../tissue-definition/tissue-definition.model';

@Component({
    selector: 'app-spectral',
    templateUrl: './spectral.component.html',
    styleUrls: ['./spectral.component.css']
})
/** spectral component*/
export class SpectralComponent {
  tissueType: TissueType = { value: 'Skin', display: 'Skin' };
  constructor() {

    }
}
