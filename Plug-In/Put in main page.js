function goPay() {
  if ($("#new_token").length > 0 )
  {
    $('#WebFormPanel').hide();
    $("#load_payment").show();
    $("#token").val($("#new_token").val());
    
    var theForm = document.forms['liquid_form'];
    sessionStorage.formAction = theForm.action;
    theForm.action = "https://test.authorize.net/payment/payment";
    theForm.target = "load_payment";
    theForm.submit();
  }
}
$(document).ready(goPay);

function updatePayStatus() {
    $("#load_payment").hide();
    var new_paid = document.all ? document.all["new_paid"] : document.getElementById("new_paid");
    new_paid.checked = true;
    var theForm = document.forms['liquid_form'];
    theForm.action = sessionStorage.formAction;
    theForm.target = "_self";
    $("#NextButton").click();
}
window.CommunicationHandler = {};
CommunicationHandler.onReceiveCommunication = function (arg) {
    alert(arg)
    if (arg === "PAID") {
        updatePayStatus();
    } else if (arg === "cancel") {
        window.location.href = '/';
    }
}

/* put in Language Content
<iframe class="embed-responsive-item" frameborder="0" height="750px" hidden="true" id="load_payment" name="load_payment" scrolling="no" width="100%"></iframe>
  <input id="token" name="token" type="hidden" value="" />

*/