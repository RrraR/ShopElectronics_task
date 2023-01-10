import React, {useState} from 'react';
import {Button, Form, Table} from "react-bootstrap";

export default function Order(props) {
    const { order, orderStatusesList, changeState} = props;
    const [status, setStatus] = useState(order.orderStatus)

    const renderItemsRow = item => (
        <tr key={item.productId}>
            <td>{item.productName}</td>
            <td>{item.qwt}</td>
        </tr>
    )

    const showOrderStatusHandler = (event) => {
        setStatus(event.target.value)
        changeState(order.orderId, event.target.value)
    }
    
    return (
        <tr key={order.orderId}>
            <td>
                <Form.Select onChange={event => showOrderStatusHandler(event, order)} value={status} aria-label="Default select example">
                    {orderStatusesList.map(
                        c => (<option key={c.id} value={c.name}>{c.name}</option>))}
                </Form.Select>
            </td>
            <td>{order.username}</td>
            <Table>
                <thead>
                <tr>
                    <th>Name</th>
                    <th>Quantity</th>
                </tr>
                </thead>
                <tbody>
                {order.orderItems.map(renderItemsRow)}


                </tbody>

            </Table>
        </tr>
    );
}