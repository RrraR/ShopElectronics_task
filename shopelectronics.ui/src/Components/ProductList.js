import React from 'react';
import Product from './Product';


export default function ProductList(props) {
    const { products, onAdd } = props;
    return (
        <main className="block product-3">
            <h2>Products</h2>
            <div className={"productlist"}>
                {products.map((product) => (
                    <Product key={product.id} product={product} onAdd={onAdd}></Product>
                ))}
            </div>
        </main>
    );
}