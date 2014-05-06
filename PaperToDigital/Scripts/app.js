$(document).foundation();

var windowWidth;
 
$(function () {

    $('#date').fdatepicker();

    $("#file").on("change",showImg);
    windowWidth = window.innerWidth;
    if(!("url" in window) && ("webkitURL" in window)) {
        window.URL = window.webkitURL;
    }

    $('#crop').click(function (e) {
        e.preventDefault();
        initJcrop();
    });
});

function scrollToElement(selector, time, verticalOffset) {
    time = typeof (time) != 'undefined' ? time : 1000;
    verticalOffset = typeof (verticalOffset) != 'undefined' ? verticalOffset : 0;
    element = $(selector);
    offset = element.offset();
    offsetTop = offset.top + verticalOffset;
    $('html, body').animate({
        scrollTop: offsetTop
    }, time);
}

function showImg(e) {
    if(e.target.files.length == 1 &&
    e.target.files[0].type.indexOf("image/") == 0) {
        $("#preview").attr("src", URL.createObjectURL(e.target.files[0]));
        $("#crop").show();
        scrollToElement('#preview', 500);
    }
}

function initJcrop() {
    $('#preview').Jcrop({
        onChange: showCoords,
        onSelect: showCoords
    });
}

function showCoords(c) {
    var image = $("img.image");
    var coords = '&crop=(' + c.x + ',' + c.y + ',' + c.x2 + ',' + c.y2 + ')&cropxunits=' + image.width() + '&cropyunits=' + image.height();
    $('#hdncoords').val(coords)
};