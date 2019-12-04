import { Component } from '@angular/core';
import { AbsorberConcentration } from '../tissue-definition/absorber-concentration.model';
import { Skin, Liver, IntralipidPhantom, BreastPostMenopause, BreastPreMenopause, Custom, BrainGrayMatter, BrainWhiteMatter } from '../tissue-definition/absorber-list';
import { BloodConcentration } from '../tissue-definition/blood-concentration.model';
import { Range } from '../range/range.model';
import { ListType } from '../shared/list-definition.model';
import { PowerLawScatterer as PowerLaw } from '../scatterer-type/power-law.model';
import { IntralipidScatterer as Intralipid } from '../scatterer-type/intralipid.model';
import { MieScatterer as MieParticle } from '../scatterer-type/mie-particle.model';
import { PlotService } from '../services/plot.service';
import * as $ from 'jquery';

@Component({
    selector: 'app-spectral',
    templateUrl: './spectral.component.html',
    styleUrls: ['./spectral.component.css']
})
/** spectral component*/
export class SpectralComponent {
  tissueTypeDropdown: ListType = { value: 'Skin', display: 'Skin' };
  absorberConcentration: Array<AbsorberConcentration> = Skin;
  bloodConcentration: BloodConcentration = { totalHb: 80, bloodVolume: 0.0344, stO2: 0.7, visible: true };
  scattererTypeDropdown: ListType = { value: 'PowerLaw', display: 'PowerLaw [A*λ^(-b)]' };
  powerLaw: PowerLaw = { a: 1.2, b: 1.42, show: true };
  intralipid: Intralipid = { volumeFraction: 0.01, show: false };
  mieParticle: MieParticle = { particleRadius: 0.5, ParticleRefractiveIndexMismatch: 1.4, MediumRefractiveIndexMismatch: 1, volumeFraction: 0.01, show: false };
  range: Range = {
    title: 'Wavelength Range',
    startLabel: 'Begin',
    startLabelUnits: 'nm',
    start: 650,
    endLabel: 'End',
    endLabelUnits: 'nm',
    stop: 1000,
    numberLabel: 'Number',
    count: 36
  };

  constructor(private plotData: PlotService) {

  }

  plotMuspSpectrum() {
    // need to update the absorber values because the input to not recognise the change
    this.updateAbsorberValues(this.tissueTypeDropdown.value);
    let spectralSettings = {
      spectralPlotType: "mua",
      plotName: "μa",
      tissueType: this.tissueTypeDropdown.value,
      absorberConcentration: this.absorberConcentration,
      bloodConcentration: this.bloodConcentration,
      scatteringType: this.scattererTypeDropdown.value,
      powerLawScatterer: this.powerLaw,
      intralipidScatterer: this.intralipid,
      mieScatterer: this.mieParticle,
      xAxis: this.range
    };
    console.log(spectralSettings);
    console.log(JSON.stringify(spectralSettings));
    this.plotData.getPlotData(spectralSettings, "spectral").subscribe((data: any) => {
      //set the plot grouping based on the checkbox value
      this.plotData.groupPlots = $("#group-plots").is(":checked");
      this.plotData.addNewPlot(data);
    });
  }

  plotMuaSpectrum() {
    // need to update the absorber values because the input to not recognize the change
    this.updateAbsorberValues(this.tissueTypeDropdown.value);
    let spectralSettings = {
      spectralPlotType: "musp",
      plotName: "μs'",
      tissueType: this.tissueTypeDropdown.value,
      absorberConcentration: this.absorberConcentration,
      bloodConcentration: this.bloodConcentration,
      scatteringType: this.scattererTypeDropdown.value,
      powerLawScatterer: this.powerLaw,
      intralipidScatterer: this.intralipid,
      mieScatterer: this.mieParticle,
      xAxis: this.range
    };
    console.log(spectralSettings);
    console.log(JSON.stringify(spectralSettings));
    this.plotData.getPlotData(spectralSettings, "spectral").subscribe((data: any) => {
      //set the plot grouping based on the checkbox value
      this.plotData.groupPlots = $("#group-plots").is(":checked");
      this.plotData.addNewPlot(data);
    });
  }

  updateAbsorberValues(value) {
    switch (value) {
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
