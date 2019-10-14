import { Component } from '@angular/core';
import { AbsorberConcentration } from '../tissue-definition/absorber-concentration.model';
import { Skin } from '../tissue-definition/absorber-list';
import { BloodConcentration } from '../tissue-definition/blood-concentration.model';
import { Range } from '../range/range.model';
import { ListType } from '../shared/list-definition.model';
import { PowerLaw } from '../scatterer-type/power-law.model';
import { Intralipid } from '../scatterer-type/intralipid.model';
import { MieParticle } from '../scatterer-type/mie-particle.model';

@Component({
    selector: 'app-spectral',
    templateUrl: './spectral.component.html',
    styleUrls: ['./spectral.component.css']
})
/** spectral component*/
export class SpectralComponent {
  tissueType: ListType = { value: 'Skin', display: 'Skin' };
  absorberConcentration: Array<AbsorberConcentration> = Skin;
  bloodConcentration: BloodConcentration = { totalHb: 80, bloodVolume: 0.0344, stO2: 0.7, visible: true };
  scattererType: ListType = { value: 'PowerLaw', display: 'PowerLaw [A*λ^(-b)]' };
  powerLaw: PowerLaw = { a: 1.2, b: 1.42, show: true };
  intralipid: Intralipid = { volumeFraction: 0.01, show: false };
  mieParticle: MieParticle = { particleRadius: 0.5, particleN: 1.4, mediumN: 1, volumeFraction: 0.01, show: false };
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
