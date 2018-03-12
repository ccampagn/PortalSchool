function validatepassword() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    if (username == "" || password == "") {
        alert("All field must be fill out")
        return false;
    }
    return true;
}
function testing() {
    document.test.submit();
}
