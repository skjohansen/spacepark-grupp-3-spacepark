/* 
    Uppstartkonfigurering
*/

var connected = false;

$(".box__content[data-content='" + 2 +"']").hide().slideUp();

$(".menu-button").on('click', function() {
    if(!$(this).hasClass("menu-button--active")) { 
        
        $(".box__error").slideUp();
        var clicked = $(this).data('button');

        $(".menu-button").each(function() { 
            $(this).toggleClass("menu-button--active");
        });

        $('.box__content[data-content]').each(function(){
            if( $(this).attr('data-content') == clicked ) {
                $(this).slideDown().show();
            }
            else {
                $(this).slideUp().hide();
            }
        });
    }
});