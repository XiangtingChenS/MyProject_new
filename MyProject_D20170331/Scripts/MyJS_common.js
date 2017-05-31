
function SubTotalChange_Editor(container, options) {
    $('<input data-bind="value:' + options.field + '"/>')
     .appendTo(container).kendoNumericTextBox({
         autoBind: true,
         change: ChangeSubTotal
     });
}

function productDropDownEditor(container, options) {
    var Product = [];
    Product.push({ ProductID: 0, ProductName: "-Select-" });
    $.ajax({
        type: 'get',
        url:'./getProduct', // '@Url.Action("getProduct")',
        async: false,
        success: function (result) {
            $.each(result, function (i, field) {
                var Myfield = field.split(',');
                Product.push({ ProductID: Myfield[0], ProductName: Myfield[1] });
            });
            console.log("ok");

        }, error: function () {
            console.log(error);
        }
    });

    $('<input required data-text-field="ProductName" data-value-field="ProductID" data-bind="value:' + options.field + '"/>')
    .appendTo(container)
    .kendoDropDownList({
        autoBind: true,
        dataTextField: "ProductName",
        dataValueField: "ProductID",
        dataSource: Product,
        change: onChange_productDropDown
    });
    disableSubTotal();
}

function disableSubTotal()
{
    //可抓所有tr
    var theGrid = $("#grid").data("kendoGrid");

    var tr = $("#grid tbody").find('tr').each(function () {
        var model = theGrid.dataItem(this);
        kendo.bind(this, model);
    });

    for (var i = 0; i < tr.length; i++) {
        tr[i].children[3].disabled = true;
    }
}


function onChange_productDropDown(e) {
    console.log(e.sender.text() + " " + e.sender.value());

    $.ajax({
        type: 'post',
        url: './getProductPrice',//'@Url.Action("getProductPrice")',
        data: {
            index: e.sender.value()
        }, success: function (result) {
            model.set("UnitPrice", result);
            ChangeSubTotal();
        }, error: function () {
            alert("error");
        }
    });
}

function ChangeSubTotal() {
    var subtotal = model.UnitPrice * model.Quantity;
    model.set("SubTotal", subtotal);
    ChangeSumTotal();
}


function ChangeSumTotal() {
    //可抓所有tr
    var theGrid = $("#grid").data("kendoGrid");
    var tr = $("#grid tbody").find('tr').each(function () {
        var model = theGrid.dataItem(this);
        kendo.bind(this, model);
    });

    var sum = 0;
    for (var i = 0; i < tr.length; i++) {
        sum += parseInt(replaceAll(tr[i].children[3].innerText));
    }
    document.getElementById("allTotal").innerHTML = "$ " + formatNumber(sum);
    disableSubTotal();
}

function replaceAll(n) {
    while (n.indexOf(",") != -1) {
        n = n.replace(",", "");
    }
    return n;
}

function formatNumber(n) {
    n += "";
    var arr = n.split(".");
    var re = /(\d{1,3})(?=(\d{3})+$)/g;
    return arr[0].replace(re, "$1,") + (arr.length == 2 ? "." + arr[1] : "");
}

function setOrderDetail(data) {
    var dataSource = new kendo.data.DataSource({
        data: data,
        schema: {
            model: {
                id: "ProductID",
                fields: {
                    Product: { defaultValue: { ProductID: "0", ProductName: "-Select-" } },
                    UnitPrice: { type: "number" },
                    Quantity: { type: "number" },
                    SubTotal: { type: "number" },
                }
            }
        }
    });

    $("#grid").kendoGrid({
        dataSource: dataSource,
        toolbar: ["create"],
        columns: [
            { field: "Product", title: "商品", editor: productDropDownEditor, template: "#=Product.ProductName#" },
            { field: "UnitPrice", title: "單價", editor: SubTotalChange_Editor, attributes: { class: "TextRight" } },
             { field: "Quantity", title: "數量", editor: SubTotalChange_Editor, attributes: { class: "TextRight" } },
            { field: "SubTotal", title: "小計", format: "{0:n0}", width: "130px", attributes: { class: "TextRight" } },
            {
                command: [{
                    name: "Delete", text: "刪除", click: function (e) {
                        e.preventDefault();
                        var tr = $(e.target).closest("tr");
                        var trData = this.dataItem(tr);

                        var dataSource = $("#grid").data("kendoGrid").dataSource;
                        dataSource.remove(trData);
                        dataSource.sync();
                        ChangeSumTotal();
                    }
                }], title: "", width: "150px"
            }],
        editable: true,
        edit: function (e) {
            model = e.model;
        }
    });

    disableSubTotal();
}


$(document).ready(function () {
    //Date Validator
    $(function () {
        var container = $("#MyForm");
        kendo.init(container);
        container.kendoValidator({
            rules: {
                greaterdate: function (input) {
                    if (input.is("[data-greaterdate-msg]") && input.val() != "") {
                        console.log(input.data("greaterdateField"));
                        var date = kendo.parseDate(input.val()),
                            otherDate = kendo.parseDate($("[name='" + input.data("greaterdateField") + "']").val());
                        return otherDate == null || otherDate.getTime() < date.getTime();
                    }
                    return true;
                },
                vaildateCombobox: function (input) {
                    if (input.is("[sign-combobox]")) {
                        if (input.data("kendoComboBox").selectedIndex == 0) {
                            return false;
                        }
                    }
                    return true;
                }
            },
            messages: {
                vaildateCombobox: "請選擇這個欄位"
            }
        });
    });

    //DataPicker
    $("#OrderDate").kendoDatePicker({
        format:"yyyy/MM/dd",
        animation: {
            close: {
                effects: "fadeOut zoom:out",
                duration: 300
            },
            open: {
                effects: "fadeIn zoom:in",
                duration: 300
            }
        }
    });
    $("#RequireDate").kendoDatePicker({
        format:"yyyy/MM/dd",
        animation: {
            close: {
                effects: "fadeOut zoom:out",
                duration: 300
            },
            open: {
                effects: "fadeIn zoom:in",
                duration: 300
            }
        }
    });
    $("#ShipDate").kendoDatePicker({
        format:"yyyy/MM/dd",
        animation: {
            close: {
                effects: "fadeOut zoom:out",
                duration: 300
            },
            open: {
                effects: "fadeIn zoom:in",
                duration: 300
            }
        }
    });

})


function showNotification(title)
{       
    var notification = $("#notification").kendoNotification({
        stacking: "top",
        templates: [{
            type: "error",
            template: $("#errorTemplate").html()
        }]
    }).data("kendoNotification");
    $(document).one("kendo:pageUnload", function () { if (notification) { notification.hide(); } });

    notification.show({
        title: title
    }, "error");
}