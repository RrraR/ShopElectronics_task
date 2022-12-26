import React, {useEffect, useState} from "react";
import api from "../api";
import Header from "../Components/Header";
import ProductList from "../Components/ProductList"
import Cart from "../Components/Cart";
import {useParams} from "react-router-dom";

function ShopPage() {
    const {id} = useParams();
    const [products, setProducts] = useState([]);
    const [cartItems, setCartItems] = useState([]);
    

    useEffect(() => {
        api.get(`Product/category/${id}/products`)
            .then(function (response) {
                setProducts(response.data);
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .finally(function () {
                // always executed
            });
    }, []);

    const onAdd = (product) => {
        const exist = cartItems.find((x) => x.id === product.id);
        if (exist) {
            setCartItems(
                cartItems.map((x) =>
                    x.id === product.id ? {...exist, qty: exist.qty + 1} : x
                )
            );
        } else {
            setCartItems([...cartItems, {...product, qty: 1}]);
        }
        const temp = cartItems.find((x) => x.id === product.id);
        // api.post(`/ShoppingCart`,
        //     {
        //         cartId: 1,
        //         productId: temp.id,
        //         qwt: temp.qwt
        //     }
        // ).then(r => console.log(r))
        
    };
    const onRemove = (product) => {
        const exist = cartItems.find((x) => x.id === product.id);
        if (exist.qty === 1) {
            setCartItems(cartItems.filter((x) => x.id !== product.id));
        } else {
            setCartItems(
                cartItems.map((x) =>
                    x.id === product.id ? {...exist, qty: exist.qty - 1} : x
                )
            );
        }
    };


    return (
        <div className="App">
            <Header countCartItems={cartItems.length}></Header>
            <div className="mainRow">
                <ProductList products={products} onAdd={onAdd}></ProductList>
                <Cart
                    cartItems={cartItems}
                    onAdd={onAdd}
                    onRemove={onRemove}
                ></Cart>
            </div>
        </div>
    );
}



export default ShopPage;