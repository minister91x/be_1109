AddItemToCart = function (e) {
    // Đọc dữ liệu từ Attribute
   
    var productId = $(e).data('productid');
    var productName = $(e).data('productname');
    var price = $(e).data('price');

    var item = {
        ProductID: productId,
        ProductName: productName,
        Price: price,
        Quantity: 1
    };

    // Đưa vào giỏ 
    var store = GetCookie('MyShoppingCart');
    Add_UpdateShoppingCart(store, item);

    alert("Thêm vào giỏ thành công!");
}


RemoveItemFromCart = function (e) {
    var result = confirm("Bạn có chắc chắn muốn xóa sản phẩm này ?");
    if (result) {
        var item = {
            ProductID: $(e).data('productid'),
            ProductName: $(e).data('productname'),
            Quantity: $(e).data('quantity'),
            Price: $(e).data('price')

        };
        var store = GetCookie('MyShoppingCart');
        store = RemoveItemFromShoppingCart(store, item);
        //UpdateShoppingCartView(store, 2);

        window.location.href = "/shoppingCart/Index";
    }
}


UpdateItemToCart = function (e) {
    debugger;
    var quantity = $("#txtQuantity").val();
    if (quantity == "" || quantity == null) { return; }

    var productNumber = parseInt(quantity, 10);
    if (productNumber <= 0) { return; }

    var item = {
        ProductID: $(e).data('productid'),
        ProductName: $(e).data('productname'),
        Quantity: parseInt(quantity, 10),
        Price: $(e).data('price')

    };

    var store = GetCookie('MyShoppingCart');

    var index = store.findIndex(function (d) {
        return d.ProductID == item.ProductID;
    });

    if (store.length == 0 || index == -1) {
        return;
    }

    store[index].Quantity = productNumber;
    SetCookie('MyShoppingCart', store);

    window.location.href = "/shoppingCart/Index";
}

Add_UpdateShoppingCart = function (store, item, quantity) {
    console.log(store);
    console.log(item);

    var index = store.findIndex(function (d) {
        return d.ProductID == item.ProductID;
    });

    if (store.length == 0 || index == -1) {
        store.push(item);
        SetCookie('MyShoppingCart', store);
        return store;
    }
    if (quantity != null && quantity != "undefined") {
        store[index].Quality = quantity;
    } else {
        store[index].Quality = parseInt(store[index].Quality) + 1;
    }
    SetCookie('MyShoppingCart', store);
    return store;
}

RemoveItemFromShoppingCart = function (store, item) {

    if (store.length > 0) {
        var index = store.findIndex(function (d) {
            return d.ProductID == item.ProductID;
        });

        store.splice(index, 1);
        SetCookie('MyShoppingCart', store);
        return store;
    }

}