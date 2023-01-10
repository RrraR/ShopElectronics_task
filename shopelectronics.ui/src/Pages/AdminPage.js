import {Table, Form, Button} from "react-bootstrap";
import 'bootstrap/dist/css/bootstrap.css';
import {useEffect, useState} from "react";
import api from "../api";
import Header from "../Components/Header";
import Order from "../Components/Order";

function AdminPage() {
    const [orders, setOrders] = useState([])
    const [orderStatusesList, setOrderStatusesList] = useState([])
    const [ordersToChange, setOrdersToChange] = useState([]);

    useEffect(() => {
        api.post('Orders/adminGetOrders')
            .then(function (response) {
                setOrders(response.data);
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            });
        
        api.get('Orders')
            .then( r => setOrderStatusesList(r.data))
            .catch(function (error) {
                // handle error
                console.log(error);
            });
        
    }, [])
    
    function changeState(id, status){
       // let orderToChange = orders.find(o => o.orderId === id);
        
        const exist = ordersToChange.find( (x) => x.orderId === id);
        if (exist) {
            setOrdersToChange(
                ordersToChange.map((x) =>
                    
                    x.orderId === id ? {...x, orderStatus: status} : x
                )
                
            );
        } else {
            setOrdersToChange([...ordersToChange, {id, orderStatus: status}]);
        }
        
    }

    function saveChangesHandler() {
        api.post('Orders/adminUpdateOrders',
            ordersToChange.map((order) => ({
                orderId: order.id,
                Status: order.orderStatus
            }))
        ).then(r => r.status === 200 ? window.location.reload(false) : console.log(r))
    }

    return (
        <>
            <Header></Header>
            <Button 
                hidden={ordersToChange.length === 0 ? true : false}
                onClick={saveChangesHandler}
            >Save changes</Button>
            <Table>
                <thead>
                <tr>
                    <th>Order status</th>
                    <th>Items in order</th>
                </tr>
                </thead>
                <tbody>
                {orders.map((order)=>(
                    <Order key={order.orderId} order={order} orderStatusesList={orderStatusesList} changeState={changeState}></Order>
                ))}

                </tbody>
            </Table>
        </>
    );
}

export default AdminPage;