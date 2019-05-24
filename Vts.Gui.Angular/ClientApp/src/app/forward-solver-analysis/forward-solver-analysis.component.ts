import { Component } from '@angular/core';
import { ForwardSolverEngine } from '../forward-solver-engine/forward-solver-engine.model';
import { GaussianBeam } from '../forward-solver-engine/gaussian-beam.model';
import { SolutionDomain } from '../solution-domain/solution-domain.model';
import { IndependentAxis } from '../solution-domain/independent-axis.model';
import { Range } from '../range/range.model';
import { OpticalProperties } from '../optical-properties/optical-properties.model';
import { ModelAnalysisType } from '../model-analysis-type/model-analysis-type.model';
import * as $ from 'jquery';
declare const generatePlot: any;
declare const createTabAndPane: any;
declare const generatePlot: any;

@Component({
  selector: 'app-forward-solver-analysis',
  templateUrl: './forward-solver-analysis.component.html',
  styleUrls: ['./forward-solver-analysis.component.css']
})
/** forward-solver-analysis component*/
export class ForwardSolverAnalysisComponent {
  forwardSolverEngine: ForwardSolverEngine = { value: 'DistributedPointSourceSDA', display: 'Standard Diffusion (Analytic: Distributed Point Source)' };
  gaussianBeam: GaussianBeam = {
    show: false,
    diameter: 0.1
  };
  solutionDomain: SolutionDomain = { value: "rofrho" };
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
    startValue: 0.5,
    endLabel: 'End',
    endLabelUnits: 'mm',
    endValue: 9.5,
    numberLabel: 'Number',
    numberValue: 19
  };
  opticalProperties: OpticalProperties = {
    mua: 0.01,
    mus: 1,
    g: 0.8,
    n: 1.4
  };
  modelAnalysisType: ModelAnalysisType = { value: 'R' };

  url = "Handlers/VtsHandler.ashx";
  currentPlotObject = [];

  constructor() {

  }

  onSubmit() {
    var fsSettings = {
      forwardSolverEngine: this.forwardSolverEngine.value,
      solutionDomain: this.solutionDomain.value,
      independentAxes: this.independentAxes,
      range: this.range,
      opticalProperties: this.opticalProperties,
      modelAnalysis: this.modelAnalysisType.value
    };
    console.log(fsSettings);
    console.log(JSON.stringify(fsSettings));
    $.ajax({
      type: "POST",
      contentType: "thislication/json",
      url: this.url + "?action=getdata",
      data: JSON.stringify(fsSettings),
      dataType: "json",
      success: function (data) {
        var isPlotted = false;
        if (this.currentPlotObject) {
          $.each(this.currentPlotObject, function (key, plotObject) {
            if (plotObject.id === data.id) {
              //for complex plots there will be multiple plots
              $.each(data.plotlist, function (i, plot) {
                this.currentPlotObject[key].plotlist.push(plot);
              });
              generatePlot(this.currentPlotObject[key].id, this.currentPlotObject[key]);
              $('#plot-tabs li').removeClass('active');
              $('#plot-column .tab-pane').removeClass('active');
              $('#pane-' + this.currentPlotObject[key].id).addClass('active');
              $('#tab-' + this.currentPlotObject[key].id).addClass('active');
              isPlotted = true;
            }
          });
        }
        if (!isPlotted) {
          var plotObject = data;
          createTabAndPane(plotObject.id, plotObject);
          generatePlot(plotObject.id, plotObject);
          this.currentPlotObject.push(plotObject);
        }
      },
      error: function (xhr, ajaxOptions, thrownError) {
        alert(xhr.status + ", " + ajaxOptions + ", " + thrownError);
      }
    });
  }

}
