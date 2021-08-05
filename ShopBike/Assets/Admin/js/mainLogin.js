function validateForm() {
    var email = document.myform.email.value;
    atpos = email.indexOf("@");
    dotpos = email.lastIndexOf(".");
    if (email == "") {
        alert("Email không được để trống !!");
        document.myform.email.focus();
        return false;
    }
    if (atpos < 1 || (dotpos - atpos < 2)) {
        alert("Nhập đúng địng dạng email.");
        document.myform.email.focus();
        return false;
    }
    return true;
}


