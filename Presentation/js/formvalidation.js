/*
    Starta ny parkering event
*/

$("#start-parking").on("click", function() {
    
    if($("#park-form-name").val() < 1 && $("#park-form-driverid").val() < 1) {
        appendError("Du måste ange namn eller förarid");
        return;
    }
});
