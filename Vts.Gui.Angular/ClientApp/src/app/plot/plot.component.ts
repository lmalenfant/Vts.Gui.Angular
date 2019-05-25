import { Component, OnInit, OnChanges } from '@angular/core';
import { PlotObject } from './plot-object.model';
import { PlotService } from '../services/plot.service';
import * as $ from 'jquery';

@Component({
  selector: 'app-plot',
  templateUrl: './plot.component.html',
  styleUrls: ['./plot.component.css']
})
/** plot component*/
export class PlotComponent implements OnInit, OnChanges {
  plotObject: PlotObject;

  plotObjects: Array<PlotObject> = [this.plotObject];

  colorArray = [];
  plot;

  constructor(private plotData: PlotService) {

  }

  ngOnInit() {
    this.plotData.currentPlotObjects.subscribe(plotObjects => this.plotObjects = plotObjects);
    this.plotData.newPlotObject.subscribe(plotObject => {
      this.plotObject = plotObject;
      if (Object.keys(this.plotObject).length !== 0) {
        this.updatePlotPanels(this.plotObject);
      }
    });
  }

  ngOnChanges() {
    console.log('changes detected');
  }

  updatePlotPanels(plotObject) {
    this.generatePlot(plotObject);
    var selector = '#pane-' + plotObject.id;
    $('#tab-' + plotObject.id + ' a').addClass('active');
    $(selector).addClass('active');
    $(selector).addClass('show');
  }

  closeTab(plot) {
    if (this.plotObjects) {
      $.each(this.plotObjects, function (key, plotObject) {
        if (plotObject.id === plot.id) {
          this.plotObjects.splice(key, 1);
          return false;
        }
        return true;
      });
    }
    $('#pane-' + plot.id).remove();
    $('#tab-' + plot.id).remove();
  }

  deletePlot(plot, key) {
    if (this.plotObjects) {
      console.log(plot.id);
      console.log(key);
      this.plotObjects.forEach(function (plotObject) {
        if (plotObject.id === plot.id) {
          console.log(plotObject);
          plotObject.plotlist.splice(key, 1); //remove the plot
          this.colorArray.splice(key, 1); //delete the color from the color array so the colors remain aligned with the plots
          this.generatePlot(plot.id, plotObject);
          return false;
        }
        return true;
      });
    }
  }

  generatePlot(plotObject) {
    var id = plotObject.id;
    var datasets = plotObject.plotlist;
    var i = 0;
    //datasets.forEach(function (key, val) {
    //  if (Object.keys(this.colorArray).length !== 0) {
    //    val.color = this.colorArray[key]; //if a value exists pull it from the color array
    //  } else {
    //    if (this.colorArray.length > 0) {
    //      i = this.colorArray[this.colorArray.length - 1] + 1;
    //    }
    //    val.color = i;
    //    this.colorArray.push(i); //create a color array so colors remain with plots when deleting plots
    //  }
    //  ++i;
    //});

    var placeholder = $("#placeholder-" + id);
    placeholder.html("");
    // insert checkboxes 
    var choiceContainer = $("#choices-" + id);
    choiceContainer.html("");
    choiceContainer.prepend('<p>' + plotObject.legend + '</p><table>');
    //$.each(datasets, function (key, val) {
    //  choiceContainer.append("<tr><td style='padding-right: 5px; vertical-align: top;'>" +
    //    "<div style='padding: 1px; border: 1px solid lightgrey; border-image: none;'><div id='color-" +
    //    key + "' style='border-image: none; width: 10px; height: 10px; overflow: hidden;'></div></div></td>" +
    //    "<td style='padding-right: 5px; vertical-align: top;'><input type='checkbox' name='" + key +
    //    "' checked='checked' id='id" + key + "'></input></td><td style='padding-right: 5px; vertical-align: top;'>" +
    //    "<label for='id" + key + "'>" + val.label + "</label>&nbsp;&nbsp;<i class=\"fas fa-times\" onclick=\"deletePlot('" + id + "', '" + key + "');\"></i></td></tr>");
    //});
    //choiceContainer.append('</table>');

    $("#choices-" + id + " input").bind('click', function () {
      this.plotAccordingToChoices(plotObject);
    });

    this.plotAccordingToChoices(plotObject);
    if (i < 2) {
      //hide the checkbox
      $("#choices-" + id + " input").hide();
    }

    //set checkbox colors
    //var series = this.plot.getData();
    //choiceContainer.find("input").each(function (key) {
    //  choiceContainer.find("#color-" + key).css("background-color", series[key].color);
    //});

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
