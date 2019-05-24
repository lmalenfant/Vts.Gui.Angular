import { Component, Input } from '@angular/core';
import { OpticalProperties } from './optical-properties.model'

@Component({
  selector: 'app-optical-properties',
  templateUrl: './optical-properties.component.html',
  styleUrls: ['./optical-properties.component.css']
})
/** optical-properties component*/
export class OpticalPropertiesComponent {
  @Input() opticalProperties: OpticalProperties;

  constructor() {

  }
}
