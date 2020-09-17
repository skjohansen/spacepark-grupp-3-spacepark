/*
    messageboxes.js
*/

$(document).on('click', '.close-error', function(){
    $(".box__error").slideUp();
});

function appendError(message) {
    $(".box__error").empty();
    $(".box__error").append(message + "<div class='close-error'></div>");
    $(".box__error").slideDown().show();
}

$(".box__error").hide().slideUp();