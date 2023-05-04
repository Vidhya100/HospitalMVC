// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Add click event listener to profile icon image
    $('.profile-icon img').click(function () {
        // Show the logout form
        $('#logout-form').show();
    });
});

//$(function () {
//    $("#datepicker").datepicker({
//        minDate: 0,
//        maxDate: "2D"
//    });
//});

$(document).ready(function () {
    var TodayDate = new Date();
    var Month = TodayDate.getMonth() + 1;
    var day = TodayDate.getDate();
    var year = TodayDate.getFullYear();
    var hrs = TodayDate.getHours();
    var minutes = TodayDate.getMinutes();

    if (day < 10)
        Month = '0' + Month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var maxDate = year + '-' + Month + '-' + day + '-'+ hrs + '-' + minutes;

    $('#datepicker').attr('min', maxDate);
    $('#datepicker1').attr('min', maxDate);

 });