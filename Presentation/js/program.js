/* 
    program.js
*/

// Configuration
var baseurl = "https://localhost:5001/api/v1.0/";

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

// Functions
function pausePage() {
    $(".loading").fadeIn();
}

function unPausePage() {
    $(".loading").fadeOut();
}

// Startup
{
    let url = baseurl + "parkinglots";
    let errormessage = "";

    $.getJSON(url, function() {
            console.log("Requesting parkinglots from API.");
        }).done(function(data) {
            console.log("Parsing items.");

            $.each(data, function(i, item) {
                $('<option value="'+item.parkinglotId+'">'+item.name+'</option>').appendTo("#park-form-parkinglot");
            });
            console.log("Done parsing items.")
        }).fail(function() {
            errormessage = "Ingen kontakt med API.";
        })
        .always(function() {
            console.log("Finished loading.");
            unPausePage();
            if (errormessage.length > 0) {
                appendError(errormessage);  
            }
    });
}

// Program
{
    var errormessage = "";
    var succesmessage = "";

    $("#start-parking").on("click", function() {
        if($("#park-form-name").val() < 1) {
            appendError("Du måste ange ditt namn");
            return;
        }
        
        errormessage = "";
        successmessage = "";

        pausePage();
        ajaxCall_ValidateDriver();
    });

    $("#pay-parking").on("click", function() {
        if($("#park-form-driverid").val() < 1) {
            appendError("Du måste ange ditt FörarId");
            return;
        } else if (!$.isNumeric($("#park-form-driverid").val())) {
            appendError("Ditt FörarId måste vara nummer");
        }
        
        errormessage = "";
        successmessage = "";

        pausePage();
        ajaxCall_ValidateDriverById();
    });

    function ajaxCall_ValidateDriverById() {
        let url = baseurl + "drivers/" + $("#pay-form-driverid").val();
        $.ajax({
            url : url,
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function(response, statusText, xhr) {
            if(xhr.status == 200) {
                console.log(statusText + ". Found driver with id " + response.driverId);
                ajaxCall_GetReceiptByDriverId(response.driverId)
            }
            else {
                appendError("Okänt fel.");
                unPausePage();
            }
        }).fail(function() {
            appendError("Ingen kontakt med API.");
            unPausePage();
        });
    }

    function ajaxCall_GetReceiptByDriverId(driverId) {
        let url = baseurl + "receipts/drivers/"+driverId;
        $.ajax({
            url: url,
            type: "GET",
            contentType: "application/json; charset=utf-8"
        }).done(function(response, statusText, xhr) {
            if(xhr.status == 200) {
                console.log(statusText + ". ReceiptId: " +response.receiptId);
            }
            unPausePage();
        }).fail(function() {
            appendError("Okänt fel.");
        });
    }

    function ajaxCall_ValidateDriver() {
        let url = baseurl + "drivers";
        $.ajax({ 
            url: url,
            data: JSON.stringify({'name': $('#park-form-name').val()}),
            type: "POST",
            contentType: "application/json; charset=utf-8"
        }).done(function( response, statusText, xhr ) {
            if(xhr.status == 201) {
                console.log(statusText + ". Driver with id " + response.driverId + " was created.");
                successmessage += "<p>Ditt Förar Id: " + response.driverId + " </p>";
                ajaxCall_FindFreeSpot(response.driverId);
            }
            else {
                appendError("Only people in Star Wars are allowed to park.");
                unPausePage();
            }
        }).fail(function() {
            appendError("Ingen kontakt med API.");
            unPausePage();
        });
    }

    function ajaxCall_FindFreeSpot(driverId) {
        let url = baseurl + 'parkinglots';
        $.ajax({
            url : url,
            data: JSON.stringify({
                ParkinglotId : $('#park-form-parkinglot').val(),
                Shipsize : $('#park-form-shipsize').val()
            }),
            type: "POST",
            contentType: "application/json; charset=utf-8"
        }).done(function(response, statusText, xhr) {
            if(xhr.status == 200) {
                console.log(statusText + ". Found parkingspot with id: " + response.parkingspotId + " for driver with Id: "+ driverId);
                successmessage += "<p>Din parkeringsplats: "+response.parkingspotId+"</p>";
                ajaxCall_CreateReceipt(response.parkingspotId, driverId);
            }
        }).fail(function(){
            appendError("Kunde inte hitta en ledig parkering som passar ditt skepps mått.");
            unPausePage();
        });
    }

    function ajaxCall_CreateReceipt(parkingspotId, driverId) {
        let url = baseurl + 'receipts';
        $.ajax({
            url : url,
            data: JSON.stringify({
                ParkingspotId : parkingspotId,
                DriverId : driverId
            }),
            type: "POST",
            contentType: "application/json; charset=utf-8"
        }).done(function(response, statusText, xhr) {
            if(xhr.status == 201) {
                console.log(statusText + ". A receipt with id " + response.receiptId + " was created.");
                successmessage += "<p>En kvittens har skrivits ut.</p>"
                ajaxCall_OccupyParkingspot(parkingspotId, driverId);
            }
        }).fail(function(){
            appendError("Okänt fel, parkeringen misslyckades.");
            unPausePage();
        });
    }

    function ajaxCall_OccupyParkingspot(parkingspotId, driverId) {
        let url = baseurl + 'parkingspots';
        $.ajax({
            url : url,
            data: JSON.stringify({
                ParkingspotId : parkingspotId,
                Occupied : "True"
            }),
            type: "PUT",
            contentType: "application/json; charset=utf-8"
        }).done(function(response, statusText, xhr) {
            if(xhr.status == 200) {
                appendSuccess("<h1>Grattis</h1><div style='margin-top: .75em;'>" + successmessage + " <p>Återvänd med ditt FörarId för att slutföra.</p></div>");
                console.log(statusText + ". Parkingspot with Id: " + parkingspotId + " was updated.");
            }
            unPausePage();
        }).fail(function(){
            appendError("Okänt fel, parkeringen misslyckades.");
            unPausePage();
        });
    }
}
