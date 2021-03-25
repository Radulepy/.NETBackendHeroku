

$(document).ready(function () {
    // see https://api.jquery.com/click/
    $("#add").click(function () {
        var newcomerName = $("#newcomer").val();

        // Remember string interpolation
        $("#list").append(`<li>${newcomerName}</li>`);

        $("#newcomer").val("");
    })
});