var userInputDelay = 250;
var inputDelay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

$.validator.addMethod("title", function (value, element) {
    return /^[A-Za-z0-9:\.\(\)\'\s]+$/i.test(value);
}, "Title must contain only letters, numbers, spaces, and symbols : . ( ) '");

$.validator.addMethod("author", function (value, element) {
    return this.optional(element) || /^[A-Za-z,\-\.\'\s]+$/i.test(value);
}, "Author must contain only letters, spaces, and symbols , - '");

$.validator.addMethod("course", function (value, element) {
    return /^[A-Za-z]+ ?[0-9]+[A-Za-z]?$/i.test(value);
}, "Invalid course format");

$.validator.addMethod("isbn", function (value, element) {
    return /^[0-9-]+[A-Za-z]?$/i.test(value);
}, "Invalid ISBN format");
