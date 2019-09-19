import { Component, Input } from '@angular/core';
import { TissueTypeList } from './tissue-list';
import { TissueType } from './tissue-definition.model';

@Component({
    selector: 'app-tissue-definition',
    templateUrl: './tissue-definition.component.html',
    styleUrls: ['./tissue-definition.component.css']
})
/** tissue-definition component*/
export class TissueDefinitionComponent {
  @Input() tissueType: TissueType;
  @Input('tissueTypeList') tissueTypeList = TissueTypeList;

  constructor() {

    }
}
