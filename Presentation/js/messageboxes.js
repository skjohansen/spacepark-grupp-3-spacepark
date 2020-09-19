/*
    messageboxes.js
*/

$(document).on('click', '.close-error', function(){
    $(".box__info--error").slideUp();
});

$(document).on('click', '.close-success', function(){
    $(".box__info--success").slideUp();
});

function appendError(message) {
    $(".box__info--error").empty();
    $(".box__info--error").append(message + "<div class='close-error'></div>");
    $(".box__info--error").slideDown().show();
    $(".box__info--success").hide().slideUp();
}

function appendSuccess(message) {
    $(".box__info--success").empty();
    $(".box__info--success").append(message + "<div class='close-success'></div>");
    $(".box__info--success").slideDown().show();
    $(".box__info--error").hide().slideUp();
}

$(".box__info--error").hide().slideUp();
$(".box__info--success").hide().slideUp();