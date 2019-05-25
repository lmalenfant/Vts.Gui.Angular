import { Injectable, OnInit } from '@angular/core';
import { PlotObject } from '../plot/plot-object.model';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable()
export class PlotService {
  plotObjects: Array<PlotObject>;
  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.currentPlotObjects.subscribe(plotObjects => this.plotObjects = plotObjects);
  }

  private allPlotDataSource = new BehaviorSubject(new Array<PlotObject>());
  currentPlotObjects = this.allPlotDataSource.asObservable();

  private singlePlotDataSource = new BehaviorSubject(new PlotObject);
  newPlotObject = this.singlePlotDataSource.asObservable();

  url = "api/Vts";

  getPlotData(fsSettings) {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'dataType': 'JSON' }),
    }
    return this.http.post(this.url, JSON.stringify(fsSettings), options);
  }

  addNewPlot(data: PlotObject) {
    this.singlePlotDataSource.next(data);
    if (typeof (this.plotObjects) === 'undefined') {
      this.plotObjects = [data];
    } else {
      this.plotObjects.push(data);
    }
    this.updatePlotData(this.plotObjects);
  }

  updatePlotData(data: Array<PlotObject>) {
    this.allPlotDataSource.next(data);
  }
}
