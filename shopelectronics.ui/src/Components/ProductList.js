import React, {useState} from 'react';
import Product from './Product';
import Pagination from "./Pagination";
import 'bootstrap/dist/css/bootstrap.css';

export default function ProductList(props) {
    const {products, onAdd} = props;
    
    return (
        <>
            <main className="block product-3">
                <h2>Products</h2>
                <div className={"productList"}>
                    {products.map((product) => (
                        <Product key={product.productId} product={product} onAdd={onAdd}></Product>
                    ))}
                </div>
            </main>
        </>
    );
}