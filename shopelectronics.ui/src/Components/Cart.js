import React from 'react';
import {Button} from "react-bootstrap";

export default function Cart(props) {
    const { cartItems, onAdd, onRemove } = props;
    const itemsPrice = cartItems.reduce((a, c) => a + c.qty * c.price, 0);
    const shippingPrice = itemsPrice > 2000 ? 0 : 20;
    const totalPrice = itemsPrice  + shippingPrice;
    
    return (
        <aside className="block cart">
            <h2>Cart Items</h2>
            <div>
                {cartItems.length === 0 && <div>Cart is empty</div>}
                {cartItems.map((item) => (
                    <div key={item.id} className="row">
                        <div className="cart">{item.name}</div>
                        <div className="cart">
                            <Button onClick={() => onRemove(item)} className="remove">
                                -
                            </Button>
                            <Button onClick={() => onAdd(item)} className="add">
                                +
                            </Button>
                        </div>

                        <div className="cart text-right">
                            {item.qty} x ${item.price.toFixed(2)}
                        </div>
                    </div>
                ))}

                {cartItems.length !== 0 && (
                    <>
                        <hr></hr>
                        <div className="cartRow">
                            <div className="cartText">Items Price</div>
                            <div className="col-1 text-right">${itemsPrice.toFixed(2)}</div>
                        </div>
                        <div className="cartRow">
                            <div className="cartText">Shipping Price</div>
                            <div className="col-1 text-right">
                                ${shippingPrice.toFixed(2)}
                            </div>
                        </div>

                        <div className="cartRow">
                            <div className="cartText">
                                <strong>Total Price</strong>
                            </div>
                            <div className="col-1 text-right">
                                <strong>${totalPrice.toFixed(2)}</strong>
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <Button onClick={() => alert('Thank you for your purchase!')}>
                                Checkout
                            </Button>
                        </div>
                    </>
                )}
            </div>
        </aside>
    );
}