import { Component, OnInit, Input } from '@angular/core';
import { Range } from './range.model';

@Component({
  selector: 'app-range',
  templateUrl: './range.component.html',
  styleUrls: ['./range.component.css'],
})
/** range component*/
export class RangeComponent implements OnInit {
  @Input() range: Range;

  constructor() {

  }

  ngOnInit() {
  }
}
