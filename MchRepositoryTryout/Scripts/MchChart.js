function getArrayWithNoRepetitions (array) {
    var lvArray = array.filter(function (elem, i, array) {
        return array.indexOf(elem) === i;
    });
    return lvArray;
}

function doAjax(url, data) {
    var options = {
        url: url,
        headers: {
            Accept: "application/json"
        },
        contentType: "application/json",
        cache: false,
        type: 'POST',
        data: data ? data : null
    };
    return $.ajax(options);
}

function createChart(data) {
    var chart = Highcharts.chart('container', {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Trens Carregados Por TU'
        },
        subtitle: {
            text: 'AMVs Utilizados'
        },
        xAxis: {
            categories: []
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Trens Carregados'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y} </b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
        },
        series: []
    });

    var categories = data.map(function (elem) {
        return 'KM ' + elem.Km;
    });

    var amvCount = [];

    for (var i = 0; i <= 3; i++) {
        amvCount.push(data.map(function (elem) {
            var myTrains = (elem.AmvsInTU[i].MchsInAmv[0].LoadedTrains.map(function (innerElem) {               
                return innerElem.TrainID;
            }));
            var lvr = getArrayWithNoRepetitions(myTrains);          
            return lvr.length;
        }));
    }
    chart.xAxis[0].setCategories(categories);

    for (var i = 1; i <= amvCount.length; i++) {
        console.log(amvCount[i - 1]);
        chart.addSeries({
            name: 'AMV ' + i,
            data: amvCount[i - 1]
        });
    }
}

function getData() {
    var sede = document.getElementById("sede").value;
    var initialDate = document.getElementById("dataInicialId").value;
    var finalDate = document.getElementById("dataFinalId").value;

    doAjax('/Home/GetDataFromTU', '{sede:"' + sede + '", initialDate: "' + initialDate + '", finalDate:"' + finalDate + '"}').
        then(function (data) {          
            createChart(data);
        },
            function (err) {
                console.log(err);
            });
}

getData();