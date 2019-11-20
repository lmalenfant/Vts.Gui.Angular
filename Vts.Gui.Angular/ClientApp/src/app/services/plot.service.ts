import { Injectable, OnInit } from '@angular/core';
import { PlotObject } from '../plot/plot-object.model';
import { BehaviorSubject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable()
export class PlotService {
  plotObjects: Array<PlotObject>;
  groupPlots: boolean;
  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.currentPlotObjects.subscribe(plotObjects => this.plotObjects = plotObjects);
  }

  private allPlotDataSource = new BehaviorSubject(new Array<PlotObject>());
  currentPlotObjects = this.allPlotDataSource.asObservable();

  private singlePlotDataSource = new BehaviorSubject(new PlotObject);
  newPlotObject = this.singlePlotDataSource.asObservable();

  baseUrl = "api/v1/";
  //url = "https://vtsapi.azurewebsites.net/api/v1/values";

  getPlotData(settings, endpoint) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'dataType': 'JSON',
        'X-API-KEY': '$RFGT%^H',
      }),
    }
    let url = this.baseUrl + endpoint;
    return this.http.post(url, JSON.stringify(settings), options);
  }

  addNewPlot(data: PlotObject) {
    if (this.groupPlots) {
      if (data.Id.startsWith("Spectral")) {
        data.Id = "Spectral";
        data.Detector = "Spectral (μa/μs')"
      } else {
        data.Id = data.YAxis + data.XAxis;
        data.Detector = "R(" + data.XAxis + ")"
      }
    }
    if (typeof (this.plotObjects) === 'undefined') {
      this.plotObjects = [data];
    } else {
      let createNewTab = true;
      this.plotObjects.forEach(function (plotObject) {
        if (plotObject.Id === data.Id) {
          data.PlotList.forEach(function (item) {
            plotObject.PlotList.push(item);
          });
          createNewTab = false;
        }
      });
      if (createNewTab) {
        this.plotObjects.push(data);
      }
    }
    this.updatePlotData(this.plotObjects);
    this.singlePlotDataSource.next(data);
  }

  updatePlotData(data: Array<PlotObject>) {
    this.allPlotDataSource.next(data);
  }
}
