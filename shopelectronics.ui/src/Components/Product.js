import React from 'react';
import {Button} from "react-bootstrap";

export default function Product(props) {
    const { product, onAdd } = props;
    return (
        <div className={"product"}>
            <img className="small" src={product.image} alt={product.name} />
            <h5>{product.name}</h5>
            <div>${product.price}</div>
            <div>{product.description}</div>
            <div>{product.brandName}</div>
            <div>
                <Button onClick={() => onAdd(product)}>Add To Cart</Button>
            </div>
        </div>
    );
}