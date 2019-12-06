import { Component, Input } from '@angular/core';
import { ModelAnalysisType } from './model-analysis-type.model';

@Component({
    selector: 'app-model-analysis-type',
    templateUrl: './model-analysis-type.component.html'
})
/** model-analysis-type component*/
export class ModelAnalysisTypeComponent {
  @Input() modelAnalysisType: ModelAnalysisType;

  constructor() {

    }
}
