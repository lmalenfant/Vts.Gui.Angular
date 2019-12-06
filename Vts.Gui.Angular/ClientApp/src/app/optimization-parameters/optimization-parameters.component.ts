import { Component, Input } from '@angular/core';
import { OptimizationParameters } from './optimization-parameters.model';

@Component({
  selector: 'app-optimization-parameters',
  templateUrl: './optimization-parameters.component.html'
})
/** optimization-parameters component*/
export class OptimizationParametersComponent {
  @Input() optimizationParameters: OptimizationParameters;

  constructor() {
    
  }
}
