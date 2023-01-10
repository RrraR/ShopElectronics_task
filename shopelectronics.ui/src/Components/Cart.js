import React, {useEffect, useState} from 'react';
import {Button} from "react-bootstrap";
import api from "../api";
import Modal from 'react-modal';
import {AuthPage} from "../Pages/AuthPage";


const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
    },
};

export default function Cart(props) {
    const {cartItems, onAdd, onRemove} = props;
    const itemsPrice = cartItems.reduce((a, c) => a + c.qwt * c.price, 0);
    const shippingPrice = itemsPrice > 2000 ? 0 : 20;
    const totalPrice = itemsPrice + shippingPrice;
    const username = localStorage.getItem("username");


    let subtitle;
    const [modalIsOpen, setIsOpen] = React.useState(false);

    function openModal() {
        setIsOpen(true);
    }

    function afterOpenModal() {
        // references are now sync'd and can be accessed.
        subtitle.style.color = '#f00';
    }

    function closeModal() {
        setIsOpen(false);
    }

    function CheckoutHandler() {
        if (username !== null){
            alert('Thank you for your purchase!')
            api.post('ShoppingCart/shop/checkout',
                cartItems.map((item) => ({
                    username: localStorage.getItem('username'),
                    ProductId: item.productId,
                    Qwt: item.qwt
                }))
            ).then(r => r.status === 200 ? window.location.reload(false) : console.log(r))
            
        }else {
            openModal()
        }

    }
    

    return (
        <aside className="block cart">
            <h2>Cart Items</h2>
            <div>
                {cartItems.length === 0 && <div>Cart is empty</div>}
                {cartItems.map((item) => (
                    <div key={item.productId} className="row">
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
                            {item.qwt} x ${item.price.toFixed(2)}
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
                        <hr/>
                        
                        <div className="row">
                            
                            <Button
                                onClick={CheckoutHandler}
                                // disabled={username === null ? true : false }
                            >
                                Checkout
                            </Button>

                            
                            <Modal
                                isOpen={modalIsOpen}
                                onAfterOpen={afterOpenModal}
                                onRequestClose={closeModal}
                                style={customStyles}
                                contentLabel="Example Modal"
                            >
                                <div> You must log in to place an order</div>
                                <AuthPage cartItems={cartItems}>
                                    
                                </AuthPage>
                            </Modal>
                        </div>
                    </>
                )}
            </div>
        </aside>
    );
}