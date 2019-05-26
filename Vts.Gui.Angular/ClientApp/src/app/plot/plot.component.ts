import { Component, OnInit, OnChanges, ViewChildren, QueryList, AfterViewInit } from '@angular/core';
import { PlotObject } from './plot-object.model';
import { PlotService } from '../services/plot.service';
import * as $ from 'jquery';
declare const plotAccordingToChoices: any;

@Component({
  selector: 'app-plot',
  templateUrl: './plot.component.html',
  styleUrls: ['./plot.component.css']
})
/** plot component*/
export class PlotComponent implements OnInit, OnChanges {
  colorArray: Array<number> = [];
  plot;
  plotObject: PlotObject;
  plotObjects: Array<PlotObject> = [this.plotObject];


  constructor(private plotData: PlotService) {

  }

  @ViewChildren('plotChoices') choices: QueryList<any>;

  ngAfterViewInit() {
    this.choices.changes.subscribe(c => {
      this.plotNewData(this.plotObjects[this.plotObjects.length - 1]);
      console.log('changes detected');
    });
  }

  ngOnInit() {
    this.plotData.currentPlotObjects.subscribe(plotObjects => {
      this.plotObjects = plotObjects;
      if (typeof (this.plotObjects) !== 'undefined' && this.plotObjects.length > 1) {
      }
    });
    this.plotData.newPlotObject.subscribe(plotObject => this.plotObject = plotObject);
  }

  ngOnChanges() {
  }

  plotNewData(plotObject) {
    this.updatePlotPanels();
    var selector = '#pane-' + plotObject.id;
    $('#tab-' + plotObject.id + ' a').addClass('active');
    $(selector).addClass('active');
    $(selector).addClass('show');
    this.generatePlot(plotObject);
  }

  updatePlotPanels() {
    $('#plot-tabs li a').removeClass('active');
    $('#plot-column .tab-pane').removeClass('active');
    $('#plot-column .tab-pane').removeClass('show');
  }

  closeTab(plot: PlotObject) {
    var self = this;
    if (this.plotObjects) {
      this.plotObjects.forEach(function (plotObject: PlotObject, key) {
        if (plotObject.id === plot.id) {
          self.plotObjects.splice(key, 1);
        }
      });
      this.plotData.updatePlotData(this.plotObjects);
    }
  }

  deletePlot(plot: PlotObject, key) {
    var self = this;
    if (this.plotObjects) {
      this.plotObjects.forEach(function (plotObject: PlotObject) {
        if (plotObject.id === plot.id) {
          console.log(plotObject);
          plotObject.plotlist.splice(key, 1); //remove the plot
          self.colorArray.splice(key, 1); //delete the color from the color array so the colors remain aligned with the plots
        }
      });
      this.plotData.updatePlotData(this.plotObjects);
    }
  }

  generatePlot(plotObject) {
    var self = this;
    var id = plotObject.id;
    var datasets = plotObject.plotlist;
    var i = 0;
    datasets.forEach(function (val, key) {
      if (typeof (self.colorArray[key]) !== 'undefined') {
        val.color = self.colorArray[key]; //if a value exists pull it from the color array
      } else {
        if (self.colorArray.length > 0) {
          i = self.colorArray[self.colorArray.length - 1] + 1;
        }
        val.color = i;
        self.colorArray.push(i); //create a color array so colors remain with plots when deleting plots
      }    
      ++i;
    });

    var choiceContainer = $("#choices-" + id);
    var placeholder = $("#placeholder-" + id);
    placeholder.html("");

    $("#choices-" + id + " input").bind('click', function () {
      this.plot = plotAccordingToChoices(plotObject);
    });

    if (placeholder) {
      this.plot = plotAccordingToChoices(plotObject);
    }
    if (i < 2) {
      //hide the checkbox
      $("#choices-" + id + " input").hide();
    }

    //set checkbox colors
    if (typeof (this.plot) !== 'undefined') {
      var series = this.plot.getData();
      choiceContainer.find("input").each(function (key) {
        choiceContainer.find("#color-" + key).css("background-color", series[key].color);
      });
    }

    placeholder.bind("plothover", function (event, pos, item) {
      var str = "(" + pos.x.toFixed(2) + ", " + pos.y.toFixed(2) + ")";
      $("#hoverdata").text(str);

      if (item) {
        var x = item.datapoint[0].toFixed(4),
          y = item.datapoint[1].toFixed(4);

        $("#tooltip").html(plotObject.legend + " = " + item.series.label + " (" + x + ", " + y + ")")
          .css({ top: item.pageY + 5, left: item.pageX + 5 })
          .fadeIn(200);
      } else {
        $("#tooltip").hide();
      }
    });


    placeholder.bind("plotclick", function (event, pos, item) {
      if (item) {
        $("#clickdata").text(" - click point " + item.dataIndex + " in " + item.series.label);
        //plot.highlight(item.series, item.datapoint);
      }
    });
  }

  plotAccordingToChoices(plotObject) {
    var id = plotObject.id;
    var datasets = $.extend(true, {}, plotObject.plotlist); //clone the dataset so we retain original values
    var data = [];

    var placeholder = $("#placeholder-" + id);
    var choiceContainer = $("#choices-" + id);
    var checkCount = choiceContainer.find("input:checked").length;
    choiceContainer.find("input:checked").each(function () {
      if (checkCount < 2) {
        $(this).prop("disabled", true);
      } else {
        $(this).prop("disabled", false);
      }
      var key = $(this).attr("name");
      if (key && datasets[key]) {
        data.push(datasets[key]);
      }
    });

    var xAxisLog = $("#xAxisLog-" + id);
    if (xAxisLog.is(':checked')) {
      //change the x-axis to a log scale
      for (var xi = 0; xi < data.length; xi++) {
        for (var xj = 0; xj < data[xi].data.length; xj++) {
          data[xi].data[xj][0] = Math.log(data[xi].data[xj][0]);
        }
      }
    }
    var yAxisLog = $("#yAxisLog-" + id);
    if (yAxisLog.is(':checked')) {
      //change the y-axis to a log scale
      for (var yi = 0; yi < data.length; yi++) {
        for (var yj = 0; yj < data[yi].data.length; yj++) {
          data[yi].data[yj][1] = Math.log(data[yi].data[yj][1]);
        }
      }
    }

    if (data.length > 0) {
      this.plot = $.plot(placeholder, data, {
        legend: { show: true },
        series: {
          lines: {
            show: true
          },
          points: {
            show: true
          }
        },
        grid: {
          hoverable: true,
          clickable: true
        }
      });

      //insert plot axis labels
      if ($("#xaxisLabel-" + id).length === 0) {
        $("<div id='xaxisLabel" + id + "' class='axisLabel xaxisLabel'></div>")
          .text(plotObject.xaxis)
          .appendTo($('#placeholder-' + id));
      }
      if ($("#yaxisLabel-" + id).length === 0) {
        $("<div id='yaxisLabel" + id + "' class='axisLabel yaxisLabel'></div>")
          .text(plotObject.yaxis)
          .appendTo($('#placeholder-' + id));
      }
    }
  }
}
