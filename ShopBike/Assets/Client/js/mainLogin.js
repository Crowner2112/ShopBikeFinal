function toggleForm() {
    container = document.querySelector(".container");
    container.classList.toggle('active');
}

window.addEventListener('scroll', function () {
    let header = document.querySelector("header");
    header.classList.toggle("sticky", window.scrollY > 10);
});

// function Dong_ho() {
//     var gio = document.getElementById("gio");
//     var phut = document.getElementById("phut");
//     var giay = document.getElementById("giay");
//     var Gio_hien_tai = new Date().getHours();
//     var Phut_hien_tai = new Date().getMinutes();
//     var Giay_hien_tai = new Date().getSeconds();
//     gio.innerHTML = Gio_hien_tai;
//     phut.innerHTML = Phut_hien_tai;
//     giay.innerHTML = Giay_hien_tai;
// }
// var Dem_gio = setInterval(Dong_ho, 1000);

var target_date = new Date().getTime() + (1000 * 3600 * 48);
var days, hours, minutes, seconds;

var countdown = document.getElementById("titles");

getCountdown();

setInterval(function () {
    getCountdown();
}, 1000);

function getCountdown() {
    var current_date = new Date().getTime();
    var seconds_left = (target_date - current_date) / 1000;

    days = pad(parseInt(seconds_left / 86400));
    seconds_left = seconds_left % 86400;

    hours = pad(parseInt(seconds_left / 3600));
    seconds_left = seconds_left % 3600;

    minutes = pad(parseInt(seconds_left / 60));
    seconds = pad(parseInt(seconds_left % 60));

    countdown.innerHTML = "<span>" + days + "</span><span>" + hours + "</span><span>"
        + minutes + "</span><span>" + seconds + "</span>";
}

function pad(n) { return (n < 10 ? '0' : '') + n; }