import { Component, OnInit } from '@angular/core';
import {FormsModule} from '@angular/forms'
import { ForwardSolverEngine } from '../forward-solver-engine/forward-solver-engine.model';
import { InverseSolverEngineList } from './inverse-solver-analysis-list';
import { GaussianBeam } from '../forward-solver-engine/gaussian-beam.model';
import { ModelAnalysisType } from '../model-analysis-type/model-analysis-type.model';
import { SolutionDomain } from '../solution-domain/solution-domain.model';
import { IndependentAxis } from '../solution-domain/independent-axis.model';
import { Range } from '../range/range.model';
import { OptimizationParameters } from '../optimization-parameters/optimization-parameters.model';
import { OptimizerType } from '../optimizer-type/optimizer-type.model';
import { OpticalProperties } from '../optical-properties/optical-properties.model';
import { PlotService } from '../services/plot.service';
import { PlotObject } from '../plot/plot-object.model';

@Component({
  selector: 'app-inverse-solver-analysis',
  templateUrl: './inverse-solver-analysis.component.html',
  styleUrls: ['./inverse-solver-analysis.component.css']
}) 

/** inverse-solver-analysis component*/
export class InverseSolverAnalysisComponent implements OnInit {
  id: string = 'Inverse';
  forwardSolverEngine: ForwardSolverEngine = { 
    value: 'PointSourceSDA', 
    display: 'Standard Diffusion (Analytic: Isotropic Point Source)'};
  gaussianBeam: GaussianBeam = {
    show: false,
    diameter: 0.1
  };
  fsEngineList = InverseSolverEngineList;
  inverseSolverEngine: ForwardSolverEngine = { 
    value: 'PointSourceSDA', 
    display: 'Standard Diffusion (Analytic: Isotropic Point Source)' };
  inverseGaussianBeam: GaussianBeam = {
    show: false,
    diameter: 0.1
  };
  isEngineList = InverseSolverEngineList;
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
  optimizationParameters: OptimizationParameters = { value: 'MuaMusp' };
  optimizerType: OptimizerType = { value: 'MPFitLevenbergMarquardt' };
  forwardOpticalProperties: OpticalProperties = {
    title: 'Forward Simulation Optical Properties',
    mua: 0.01,
    musp: 1,
    g: 0.8,
    n: 1.4
  };
  initialGuessOpticalProperties: OpticalProperties = {
    title: 'Initial Guess Optical Properties',
    mua: 0.01,
    musp: 1,
    g: 0.8,
    n: 1.4
  };
  modelAnalysisType: ModelAnalysisType = { value: 'R'};
  noiseValue = '0';
  
  measuredData: number[][];

  plotObject: PlotObject; 
  //plotObjects: Array<PlotObject>;

  constructor(private plotData: PlotService) {
  }

  ngOnInit() {
    this.plotData.newPlotObject.subscribe(plotObject => this.plotObject = plotObject);
  }


  plotMeasured() {    
    var fsSettings = {
      forwardSolverType: this.forwardSolverEngine.value,
      solutionDomain: this.solutionDomain.value,
      independentAxes: this.independentAxes,
      xAxis: this.range,
      opticalProperties: this.forwardOpticalProperties,
      modelAnalysis: this.modelAnalysisType.value,
      noiseValue: this.noiseValue
    };
    console.log(fsSettings);
    console.log(JSON.stringify(fsSettings));
    this.plotData.getPlotData(fsSettings, "forward").subscribe((data: any) => {
      //this.plotObject = data;
      this.plotData.addNewPlot(data);
      this.measuredData = data.PlotList[0].Data;
    });
  }
    
  plotInitialGuess() {    
    var igSettings = {
      forwardSolverType: this.inverseSolverEngine.value,
      forwardOpticalProperties: this.forwardOpticalProperties,
      solutionDomain: this.solutionDomain.value,
      independentAxes: this.independentAxes,
      xAxis: this.range,
      opticalProperties: this.initialGuessOpticalProperties,
      modelAnalysis: this.modelAnalysisType.value,
      noiseValue: "0"
    };
    console.log(igSettings);
    console.log(JSON.stringify(igSettings));
    this.plotData.getPlotData(igSettings, "forward").subscribe((data: any) => {
      //this.plotObject = data;
      this.plotData.addNewPlot(data);
    });
  }

  runInverse() {    
    var inSettings = {
      inverseSolverType: this.inverseSolverEngine.value,
      optimizerType: this.optimizerType.value,
      optimizationParameters: this.optimizationParameters.value,
      solutionDomain: this.solutionDomain.value,
      measuredData: this.measuredData,
      independentAxes: this.independentAxes,
      xAxis: this.range,
      opticalProperties: this.initialGuessOpticalProperties,
    };
    console.log(inSettings);
    console.log(JSON.stringify(inSettings));
    this.plotData.getPlotData(inSettings, "inverse").subscribe((data: any) => {
      //this.plotObject = data;
      this.plotData.addNewPlot(data);
    });
  }
} 
