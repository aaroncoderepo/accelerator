var dialog, dialogStudent, form, formstudent;

$(function () {
    // From http://www.whatwg.org/specs/web-apps/current-work/multipage/states-of-the-type-attribute.html#e-mail-state-%28type=email%29
    var emailRegex = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/,
       name = $("#className"),
       location = $("#location"),
       teacherName = $("#teacherName"),
       allFields = $([]).add(name).add(location).add(teacherName),
       tips = $(".validateTips");

    function updateTips(t) {
        tips
          .text(t)
          .addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }

    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Length of " + n + " must be between " +
              min + " and " + max + ".");
            return false;
        } else {
            return true;
        }
    }

    function addSchoolClass() {
        var valid = true;
        allFields.removeClass("ui-state-error");
        valid = valid && checkLength(name, "class name", 3, 100);
        valid = valid && checkLength(location, "location", 3, 100);


        if (valid) {
            $.ajax("/Home/AddClass/", {
                "dataType": "json",
                "cache": false,
                "type": "POST",
                "data": { className: name.val(), location: location.val(), teacherName: teacherName.val(), __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val() },
                "success": function (data) {
                    angular.element(document.getElementById('SchoolClasses')).scope().school.getClasses();
                    dialog.dialog("close");
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }

        return valid;
    }

    dialog = $("#dialog-form").dialog({
        dialogClass: "no-close",
        autoOpen: false,
        height: 400,
        width: 400,
        modal: true,
        buttons: {
            "Add class": addSchoolClass,
            Cancel: function () {
                form[0].reset();
                dialog.dialog("close");
            }
        },
        close: function () {
            form[0].reset();
            allFields.removeClass("ui-state-error");
        }
    });

    form = dialog.find("form").on("submit", function (event) {
        event.preventDefault();
        addSchoolClass();
    });
});

$(document).on('click', ".AddClass", function (e) {
    dialog.dialog("open");
    return false;
});

$(function () {
    // From http://www.whatwg.org/specs/web-apps/current-work/multipage/states-of-the-type-attribute.html#e-mail-state-%28type=email%29
    var emailRegex = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/,
       classId = $("#classId"),
       name = $("#studentName"),
       studentAge = $("#studentAge"),
       studentGPA = $("#studentGPA"),
       allFields = $([]).add(classId).add(name).add(studentAge).add(studentGPA),
       tips = $(".validateTips");

    function updateTips(t) {
        tips
          .text(t)
          .addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }

    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Length of " + n + " must be between " +
              min + " and " + max + ".");
            return false;
        } else {
            return true;
        }
    }

    function isNumber(o, n) {
        if (isNaN(o.val())) {
            o.addClass("ui-state-error");
            updateTips(n + " must be numeric.");
            return false; 
        }
        else {
            return true;
        }
    }

    function addSchoolClass() {
        var valid = true;
        allFields.removeClass("ui-state-error");
        valid = valid && checkLength(name, "class name", 3, 100);
        valid = valid && isNumber(studentAge, "studentAge");
        valid = valid && isNumber(studentGPA, "studentGPA");

        if (valid) {
            $.ajax("/Home/AddStudent/", {
                "dataType": "json",
                "cache": false,
                "type": "POST",
                "data": { classId: classId.val(), studentName: name.val(), studentAge: studentAge.val(), studentGPA: studentGPA.val(), __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val() },
                "success": function (data) {
                    angular.element(document.getElementById('SchoolClasses')).scope().school.getStudentsByClass(classId);
                    dialogStudent.dialog("close");
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }

        return valid;
    }

    dialogStudent = $("#dialog-form-student").dialog({
        dialogClass: "no-close",
        autoOpen: false,
        height: 400,
        width: 400,
        modal: true,
        buttons: {
            "Add student": addSchoolClass,
            Cancel: function () {
                form[0].reset();
                dialogStudent.dialog("close");
            }
        },
        close: function () {
            formstudent[0].reset();
            allFields.removeClass("ui-state-error");
        }
    });

    formstudent = dialogStudent.find("form").on("submit", function (event) {
        event.preventDefault();
        addSchoolClass();
    });
});

$(document).on('click', ".AddStudent", function (e) {
    dialogStudent.dialog("open");
    return false;
});