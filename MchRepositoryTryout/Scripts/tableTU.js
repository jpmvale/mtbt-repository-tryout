var mchLinks = document.getElementsByClassName('mchDetail'); //take all links ref from the page
var amvLinks = document.getElementsByClassName('amvDetail');

function clickEvent(element, className) { // makes the itens of the table not visiblie
    var link = element;
    var th = link.parentElement;
    var tr = th.parentElement;
    var tb = tr.parentElement; // access the superclass table

    var lines = tb.getElementsByClassName(className);

    for (var i = 0; i < lines.length; i++) // if the display is active the set it to none, else set it to active
        lines[i].style.display = lines[i].style.display == '' ? 'none' : '';
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