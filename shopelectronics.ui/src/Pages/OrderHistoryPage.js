import 'bootstrap/dist/css/bootstrap.css';
import {useEffect, useState} from "react";
import api from "../api";
import {Table} from "react-bootstrap";
import Header from "../Components/Header";

function OrderHistoryPage() {
    const [orders, setOrders] = useState([])
    // const [books, setBooks] = useState([]);
    // const [bookList, setBookList] = useState([]);
    // const [query, setQuery] = useState('');

    useEffect(() => {
        api.post('Orders/getOrders', {
            username: localStorage.getItem("username")
        })
            .then(function (response) {
                setOrders(response.data);
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .finally(function () {
                // always executed
            });
    }, [])

    // function search(books) {
    //     const search_parameters = ["name"];
    //     return books.filter(
    //         (book) =>
    //             search_parameters.some((parameter) =>
    //                 book[parameter].toString().toLowerCase().includes(query)
    //             )
    //     );
    // }
    //
    // function setQueryHandler(e) {
    //     const data = Object.values(bookList)
    //
    //     setQuery(e.target.value)
    //     setBooks(search(data))
    // }

    const renderItemsRow = item => (
        <tr key={item.productId}>
            <td>{item.productName}</td>
            <td>{item.qwt}</td>
        </tr>
    )


    const renderRow = order => (
        <tr key={order.orderId}>
            <td>{order.orderStatus}</td>
            <Table>
                <thead>
                <tr>
                    <th>Name</th>
                    <th>Qwt</th>
                </tr>
                </thead>
                <tbody>
                {order.orderItems.map(renderItemsRow)}
                

                </tbody>

            </Table>
        </tr>
    )

    return (
        <>
            <Header></Header>
            <Table>
                <thead>
                <tr>
                    <th>Order status</th>
                    <th>Items in order</th>
                </tr>
                </thead>
                <tbody>
                {orders.map(renderRow)}

                </tbody>
            </Table>
        </>
    );
}

export default OrderHistoryPage;