var mchLinks = document.getElementsByClassName('mchDetail'); //take all links ref from the page
var amvLinks = document.getElementsByClassName('amvDetail');

function _dateToSimpleString(date) {

    var day = date.getDate();
    if (day.toString().length == 1)
        day = "0" + day;
    var month = date.getMonth() + 1;
    if (month.toString().length == 1)
        month = "0" + month;
    var year = date.getFullYear();
    return day + "/" + month + "/" + year;
}

function _getTimeFromDate(date) {

    var hour = date.getHours();
    if (hour.toString().length == 1)
        hour = "0" + hour;
    var minutes = date.getMinutes();
    if (minutes.toString().length == 1)
        minutes = "0" + minutes;
    var seconds = date.getSeconds();
    if (seconds.toString().length == 1)
        seconds = "0" + seconds;
    return hour + ":" + minutes + ":" + seconds;
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

function createModal(content) {

    var modal = new tingle.modal({
        footer: true,
        stickyFooter: false,
        closeMethods: ['overlay', 'button', 'escape'],
        closeLabel: "Close",
        cssClass: ['custom-class-1', 'custom-class-2'],
        onOpen: function () {

        },
        onClose: function () {

        },
        beforeClose: function () {
            return true; // close the modal           
        }
    });

    modal.setContent(content);
    return modal;
}

function clickEvent(element, className) { // makes the itens of the table not visiblie
    var link = element;
    var th = link.parentElement;
    var tr = th.parentElement;
    var tb = tr.parentElement; // access the superclass table

    var lines = tb.getElementsByClassName(className);

    for (var i = 0; i < lines.length; i++) // if the display is active the set it to none, else set it to active
        lines[i].style.display = lines[i].style.display == '' ? 'none' : '';
}

function details(initialDate, finalDate, amv, tu, mode) {

    doAjax('/Home/GetTrainsByAmv', '{km:' + tu + ', amv: ' + amv + ', initialDate: "' + initialDate + '", finalDate:"' + finalDate + '", mode: ' + mode + '}').
        then(function (data) {
            if (data) {
                var table = document.createElement("table");
                table.classList.add("table");
                var header = table.insertRow();
                header.classList.add('tableHeader');
                var tdTrainHeader = header.insertCell();
                var tdTrainName = header.insertCell();
                var tdDataOcup = header.insertCell();
                var tdHoraOcup = header.insertCell();
                var tdTrack = header.insertCell();
                tdTrainHeader.innerText = "Id Trem";
                tdTrainName.innerText = "Nome Trem";
                tdDataOcup.innerText = "Data de Ocupação";
                tdHoraOcup.innerText = "Hora de Ocupação";
                tdTrack.innerText = "Linha";

                for (var i = 0; i < data.length; i++) {
                    var row = table.insertRow();
                    var tdTrain = row.insertCell();
                    var tdTrainName = row.insertCell();
                    var tdDataOcup = row.insertCell();
                    var tdHoraOcup = row.insertCell();
                    var tdTrack = row.insertCell();
                    tdTrain.innerText = data[i].TrainID;
                    tdTrainName.innerText = data[i].Train.TrainName;
                    tdDataOcup.innerText = _dateToSimpleString(new Date(parseInt(data[i].OcupationDate.substring(6))));
                    tdHoraOcup.innerText = _getTimeFromDate(new Date(parseInt(data[i].OcupationDate.substring(6))));
                    tdTrack.innerText = data[i].Track;
                }
                createModal(table).open();
            }
        },
            function (err) {
                alert(err);
            });
}
for (var i = 0; i < mchLinks.length; i++) //add the click event function to all links
    mchLinks[i].addEventListener('click', function () { clickEvent(this, 'mchLine') });
for (var i = 0; i < amvLinks.length; i++) //add the click event function to all links
    amvLinks[i].addEventListener('click', function () { clickEvent(this, 'amvLine') });

var allMchLines = document.getElementsByClassName('mchLine'); // get all lines from the mch table that will not be shown
var allAmvLines = document.getElementsByClassName('amvLine');

for (var i = 0; i < allMchLines.length; i++) // set the display to none
    allMchLines[i].style.display = '';
for (var i = 0; i < allAmvLines.length; i++) // set the display to none
    allAmvLines[i].style.display = 'none';