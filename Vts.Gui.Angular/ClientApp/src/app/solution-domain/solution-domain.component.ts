import { Component, Input } from '@angular/core';
import { IndependentAxis } from './independent-axis.model';
import { Range } from '../range/range.model';
import { SolutionDomain } from './solution-domain.model';

@Component({
  selector: 'app-solution-domain',
  templateUrl: './solution-domain.component.html',
  styleUrls: ['./solution-domain.component.css'],
})
/** solution-domain component*/
export class SolutionDomainComponent {
  @Input() solutionDomain: SolutionDomain;
  @Input() independentAxes: IndependentAxis;
  @Input() range: Range;

  setIndependentAxes(first, units1, second, units2) {
    console.log('setIndependentAxis');
    if (first && second && units1 && units2) {
      this.independentAxes.label = second;
      this.independentAxes.units = units2;
      this.independentAxes.first = first;
      this.independentAxes.second = second;
      this.independentAxes.firstUnits = units1;
      this.independentAxes.secondUnits = units2;
      this.independentAxes.show = true;
      this.changeUnits(2);
    } else if (first && units1) {
      this.independentAxes.units = units1;
      this.independentAxes.first = first;
      this.independentAxes.firstUnits = units1;
      this.independentAxes.show = false;
      this.changeUnits(0);
    } else {
      this.independentAxes.show = false;
    }
  }

  changeUnits(val) {
    console.log('changeUnits');
    if (val === 1) {
      this.independentAxes.units = this.independentAxes.firstUnits;
      this.range.startLabelUnits = this.independentAxes.secondUnits;
      this.range.endLabelUnits = this.independentAxes.secondUnits;
    } else if (val === 2) {
      this.independentAxes.units = this.independentAxes.secondUnits;
      this.range.startLabelUnits = this.independentAxes.firstUnits;
      this.range.endLabelUnits = this.independentAxes.firstUnits;
    } else {
      this.range.startLabelUnits = this.independentAxes.units;
      this.range.endLabelUnits = this.independentAxes.units;
    }
    switch (this.range.startLabelUnits) {
      case 'mm':
        this.range.title = 'Detector Positions';
        this.range.startValue = 0.5;
        this.range.endValue = 9.5;
        this.range.numberValue = 19;
        break;
      case '1/mm':
        this.range.title = 'Spacial Frequencies';
        this.range.startValue = 0;
        this.range.endValue = 0.5;
        this.range.numberValue = 51;
        break;
      case 'ns':
        this.range.title = 'Detection Times';
        this.range.startValue = 0;
        this.range.endValue = 0.05;
        this.range.numberValue = 51;
        break;
      case 'GHz':
        this.range.title = 'Temporal Frequencies';
        this.range.startValue = 0;
        this.range.endValue = 0.5;
        this.range.numberValue = 51;
        break;
      default:
        this.range.title = 'Detector Positions';
    }
  }

  constructor() {

  }
}
