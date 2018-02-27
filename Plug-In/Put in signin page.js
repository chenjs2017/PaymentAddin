<script>
    /*add to Portals > Content Snippets > Account/SignIn/PageCopy*/
    function setProvider( )
  {
    var email = $('#email').val();
    var domain = email.replace(/.*@/, "").toLowerCase();
    var btn = $('[name="provider"]');
    var url = '';
    if (domain=="simplecommerce.onmicrosoft.com") {
        url = "https://login.windows.net/7c09a4f5-e3df-49eb-8e15-df34fafe0f35/"
    }else if (domain =="hartogjacobs.com") {
        url = "https://login.windows.net/d9fe1264-2429-47be-a5ab-54764d05c8ff/";
    }
    if (url !== '') {
        btn.val(url);
    //btn.attr('id',url);
    alert(btn.attr('id'));
      return true;
    }
    else {
        alert("Please input a legal email address...")
      return false ;
    }
  }
  function showEmail()
  {
    var btn = $('[name="provider"]');
    btn.before('<div class="form-group"><label class="col-sm-4 control-label required" for="email"><span class="xrm-editable-text xrm-attribute" data-languagecontext="English"><span class="xrm-attribute-value-encoded xrm-attribute-value">Email address </span></span></label><div class="col-sm-8"><input aria-label="Email is a required field." class="form-control" id="email" name="email" type="text" value="" aria-required="true"></div></div>');
    btn.before('<button name="provider" type="submit" class="btn btn-primary btn-line" id="https://login.windows.net/d9fe1264-2429-47be-a5ab-54764d05c8ff/" title="Sign in with your Azure AD account." value="https://login.windows.net/d9fe1264-2429-47be-a5ab-54764d05c8ff/">Azure AD		</button>');
    btn.click(setProvider);
  }
  $(document).ready(showEmail);
</script>