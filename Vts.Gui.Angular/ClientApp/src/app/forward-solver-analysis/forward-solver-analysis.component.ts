import { Component, OnInit } from '@angular/core';
import { ForwardSolverEngine } from '../forward-solver-engine/forward-solver-engine.model';
import { GaussianBeam } from '../forward-solver-engine/gaussian-beam.model';
import { SolutionDomain } from '../solution-domain/solution-domain.model';
import { IndependentAxis } from '../solution-domain/independent-axis.model';
import { Range } from '../range/range.model';
import { OpticalProperties } from '../optical-properties/optical-properties.model';
import { ModelAnalysisType } from '../model-analysis-type/model-analysis-type.model';
import { PlotService } from '../services/plot.service';
import { PlotObject } from '../plot/plot-object.model';
import * as $ from 'jquery';

@Component({
  selector: 'app-forward-solver-analysis',
  templateUrl: './forward-solver-analysis.component.html',
  styleUrls: ['./forward-solver-analysis.component.css']
})
/** forward-solver-analysis component*/
export class ForwardSolverAnalysisComponent implements OnInit {
  id: string = 'Forward';
  forwardSolverEngine: ForwardSolverEngine = { value: 'DistributedPointSourceSDA', display: 'Standard Diffusion (Analytic: Distributed Point Source)' };
  gaussianBeam: GaussianBeam = {
    show: false,
    diameter: 0.1
  };
  solutionDomain: SolutionDomain = { value: 'ROfRho' };
  independentAxes: IndependentAxis = {
    show: false,
    first: 'ρ',
    second: 't',
    label: 't',
    value: 0.05,
    units: 'ns',
    firstUnits: 'mm',
    secondUnits: 'ns'
  };
  range: Range = {
    title: 'Detector Positions',
    startLabel: 'Begin',
    startLabelUnits: 'mm',
    start: 0.5,
    endLabel: 'End',
    endLabelUnits: 'mm',
    stop: 9.5,
    numberLabel: 'Number',
    count: 19
  };
  opticalProperties: OpticalProperties = {
    title: 'Optical Properties',
    mua: 0.01,
    musp: 1,
    g: 0.8,
    n: 1.4
  };
  modelAnalysisType: ModelAnalysisType = { value: 'R' };
  noiseValue = '0'; // always set to 0 for fs

  plotObject: PlotObject;
  //plotObjects: Array<PlotObject>;

  constructor(private plotData: PlotService) {

  }

  ngOnInit() {
    this.plotData.newPlotObject.subscribe(plotObject => this.plotObject = plotObject);
  }

  onSubmit() {
    var fsSettings = {
      forwardSolverType: this.forwardSolverEngine.value,
      solutionDomain: this.solutionDomain.value,
      independentAxes: this.independentAxes,
      xAxis: this.range,
      opticalProperties: this.opticalProperties,
      modelAnalysis: this.modelAnalysisType.value,
      noiseValue: this.noiseValue
    };
    console.log(fsSettings);
    console.log(JSON.stringify(fsSettings));
    this.plotData.getPlotData(fsSettings, "forward").subscribe((data: any) => {
      //set the plot grouping based on the checkbox value
      this.plotData.groupPlots = $("#group-plots").is(":checked");
      this.plotData.addNewPlot(data);
    });
  }
}
