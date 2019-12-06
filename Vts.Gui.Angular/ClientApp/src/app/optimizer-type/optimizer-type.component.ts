import { Component, Input } from '@angular/core';
import { OptimizerType } from './optimizer-type.model';

@Component({
  selector: 'app-optimizer-type',
  templateUrl: './optimizer-type.component.html'
})
/** optimizer-type component*/
export class OptimizerTypeComponent {
  @Input() optimizerType: OptimizerType;

  constructor() {
    
  }
}
