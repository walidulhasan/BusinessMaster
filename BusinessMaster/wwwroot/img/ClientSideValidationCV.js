jQuery.validator.addMethod("country", 
function (value, element, param) {

    if (value != "USA" && value != "UK" 
&& value != "India") {
        return false;
    }
    else {
        return true;
    }
});

jQuery.validator.unobtrusive.adapters.addBool("country");