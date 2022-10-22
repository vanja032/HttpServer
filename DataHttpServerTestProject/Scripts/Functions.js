$(document).ready(function(){
    $.ajax({
        url: "http://178.149.250.113:1234/test/",
        type: "post",
        dataType: "text",
        data: "test" ,
        success: function (response) {
            console.log("A");
           // You will get response from your PHP page (what you echo or print)
        },
        error: function(jqXHR, textStatus, errorThrown) {
           console.log(textStatus, errorThrown);
        }
    });
});