function plotAccordingToChoices(plotObject) {
  var plot;
  var id = plotObject.Id;
  var datasets = [];
  plotObject.PlotList.forEach(function (val) {
    var item = jQuery.extend(true, {}, val);
    datasets.push({ data: item.Data, label: item.Label, color: item.color });
  });
  //var datasets = jQuery.extend(true, {}, plotObject.PlotList); //clone the dataset so we retain original values
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
        data[xi].Data[xj][0] = Math.log(data[xi].data[xj][0]);
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
    plot = $.plot(placeholder, data, {
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
        .text(plotObject.XAxis)
        .appendTo($('#placeholder-' + id));
    }
    if ($("#yaxisLabel-" + id).length === 0) {
      $("<div id='yaxisLabel" + id + "' class='axisLabel yaxisLabel'></div>")
        .text(plotObject.YAxis)
        .appendTo($('#placeholder-' + id));
    }
  }
  return plot;
}

