//Load Data function  
function loadData() {
    $.ajax({
        url: "/Product/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ProductID + '</td>';
                html += '<td>' + item.ProductName + '</td>';
                html += '<td>' + item.ProductDescription + '</td>';
                html += '<td>' + item.Price + '</td>';
                html += '<td><a href="#" onclick="return getByID(' + item.ProductID + ')">Edit</a> | <a href="#" onclick="Delete(' + item.ProductID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function Add() {
    var res = validate();

    if (res == false) {
        return false;
    }

    var productObj = {
        ProductName: $('#ProductName').val(),
        ProductDescription: $('#ProductDescription').val(),
        Price: $('#Price').val()
    };

    $.ajax({
        url: "/Product/Add",
        data: JSON.stringify(productObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon Employee ID  
function getByID(productID) {
    $('#ProductName').css('border-color', 'lightgrey');
    $('#ProductDescription').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
    $('#ProductID').css('border-color', 'lightgrey');

    $.ajax({
        url: "/Product/GetbyID/" + productID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ProductID').val(result.ProductID);
            $('#ProductName').val(result.ProductName);
            $('#ProductDescription').val(result.ProductDescription);
            $('#Price').val(result.Price);

            $("#myModalLabel").text("Edit Product");
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record  
function Update() {
    var res = validate();

    if (res == false) {
        return false;
    }

    var productObj = {
        ProductID: $('#ProductID').val(),
        ProductName: $('#ProductName').val(),
        ProductDescription: $('#ProductDescription').val(),
        Price: $('#Price').val()
    };

    $.ajax({
        url: "/Product/Update",
        data: JSON.stringify(productObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ProductID').val("");
            $('#ProductName').val("");
            $('#ProductDescription').val("");
            $('#Price').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");

    if (ans) {
        $.ajax({
            url: "/Product/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ProductID').val("");
    $('#ProductName').val("");
    $('#ProductDescription').val("");
    $('#Price').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#ProductName').css('border-color', 'lightgrey');
    $('#ProductDescription').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
    $('#ProductID').css('border-color', 'lightgrey');
    $('#ProductID').hide();
    $("#myModalLabel").text("Add Product");
};

//Valdidation using jquery  
function validate() {
    var isValid = true;

    if ($('#ProductName').val().trim() == "") {
        $('#ProductName').css('border-color', 'orangered');
        isValid = false;
    }
    else {
        $('#ProductName').css('border-color', 'lightgrey');
    }

    if ($('#ProductDescription').val().trim() == "") {
        $('#ProductDescription').css('border-color', 'orangered');
        isValid = false;
    }
    else {
        $('#ProductDescription').css('border-color', 'lightgrey');
    }

    if ($('#Price').val().trim() == "") {
        $('#Price').css('border-color', 'orangered');
        isValid = false;
    }
    else {
        $('#Price').css('border-color', 'lightgrey');
    }

    return isValid;
};