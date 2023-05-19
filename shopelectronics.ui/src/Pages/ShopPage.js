import React, {useEffect, useState} from "react";
import api from "../api";
import Header from "../Components/Header";
import ProductList from "../Components/ProductList"
import Cart from "../Components/Cart";
import {useParams} from "react-router-dom";
import Pagination from "../Components/Pagination";
import {Button, Form} from "react-bootstrap";

function ShopPage() {
    const {id} = useParams();
    
    const [productsToDisplay, setProductsToDisplay] = useState([]);
    const [allProducts, setAllProducts] = useState([]);
    
    const [cartItems, setCartItems] = useState([]);
    const username = localStorage.getItem("username");

    const [searchQuery, setSearchQuery] = useState('');
    const [filterQuery, setFilterQuery] = useState('');
    
    const [currentPage, setCurrentPage] = useState(1);
    const [recordsPerPage] = useState(3);
    const indexOfLastRecord = currentPage * recordsPerPage;
    const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
    const currentRecords = productsToDisplay.slice(indexOfFirstRecord,
        indexOfLastRecord);
    const nPages = Math.ceil(productsToDisplay.length / recordsPerPage)
    const uniqueBrands = [];
    

    useEffect(() => {
        api.get(`Product/category/${id}/products`)
            .then(function (response) {
                setProductsToDisplay(response.data);
                setAllProducts(response.data);
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .finally(function () {
                // always executed
            });

        api.post('ShoppingCart/shop/getitems', {
            username: localStorage.getItem("username")
        })
            .then(
                function setResult(r) {
                    if (r.status === 200) {
                        setCartItems(r.data)
                    }
                }
            )
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .finally(function () {
                // always executed
            });

    }, []);

    useEffect(()=>{
        allProducts.map((item) => {
            let findItem = uniqueBrands.find((x) => x.brandName === item.brandName);
            if (!findItem) uniqueBrands.push(item);
        });
    }, [allProducts])
    

    const onAdd = (product) => {
        const exist = cartItems.find((x) => x.productId === product.productId);
        if (exist) {
            setCartItems(
                cartItems.map((x) =>
                    x.productId === product.productId ? {...exist, qwt: exist.qwt + 1} : x
                )
            );
            if (username !== null) {
                addItem(exist, exist.qwt + 1)
            }
        } else {
            setCartItems([...cartItems, {...product, qwt: 1}]);
            if (username !== null) {
                addItem(product, 1)
            }
        }
    };

    function addItem(product, qwt) {
        let temp = [product]
        api.post('ShoppingCart/shop/additem',
            temp.map((item) => ({
                username: localStorage.getItem("username"),
                productId: item.productId,
                qwt: qwt
            }))
        ).then(r => console.log(r))
    }


    const onRemove = (product) => {
        const exist = cartItems.find((x) => x.productId === product.productId);
        if (exist.qwt === 1) {
            if (username !== null) {
                updateQwt(exist)
            }
            setCartItems(cartItems.filter((x) => x.productId !== product.productId));
            // removeItem(exist)
        } else {
            if (username !== null) {
                updateQwt(exist)
            }
            setCartItems(
                cartItems.map((x) =>
                    x.productId === product.productId ? {...exist, qwt: exist.qwt - 1} : x
                )
            );
        }
    };

    function updateQwt(product) {
        api.patch(`ShoppingCart`, {
            username: localStorage.getItem("username"),
            productId: product.productId,
            qwt: 1
        }).then(r => console.log(r))
    }

    function search(productsToDisplay) {
        const search_parameters = ["name", "description"];
        return productsToDisplay.filter(
            (product) =>
                search_parameters.some((parameter) =>
                    product[parameter].toString().toLowerCase().includes(searchQuery))
        );
    }

    useEffect(()=>
    {
        const data = Object.values(allProducts)
        setProductsToDisplay(search(data))
    }, [searchQuery])
    
    
    useEffect(()=>
    {
        const data = Object.values(allProducts)
        setProductsToDisplay(filter(data))
    }, [filterQuery])
    
    
    function filter(productsToDisplay) {
        const filter_parameters = ["brandName"];
        return productsToDisplay.filter(
            (product) =>
                filter_parameters.some((parameter) =>
                    product[parameter].toString().toLowerCase().includes(filterQuery))
        );
    }

    return (
        <div className="App">
            <Header countCartItems={cartItems.length}></Header>

            <Button
            onClick={() => console.log(allProducts)}>
                allProducts
            </Button>

            <Button
                onClick={() => console.log(productsToDisplay)}>
                productsToDisplay
            </Button>

            <Button
                onClick={() => console.log(uniqueBrands)}>
                uniqueBrands
            </Button>
            
            <div>
                <input
                    name="search-form"
                    id="search-form"
                    className="search-input"
                    type="search"
                    placeholder='Search products'
                    onInput={(e)=>setSearchQuery(e.target.value)}
                />

                <Form.Select
                    onChange={(e)=>setFilterQuery(e.target.value)}
                    value={filterQuery}
                    aria-label="Default select example">
                    <option value=''>Show all</option>
                    {uniqueBrands.map(
                        c => (
                            <option key={c.productId} value={c.brandName.toString()}>{c.brandName.toString()}</option>))
                    }
                </Form.Select>


            </div>

            <div className="mainRow">

                <ProductList
                    products={currentRecords}
                    onAdd={onAdd}
                ></ProductList>
                <Cart
                    cartItems={cartItems}
                    onAdd={onAdd}
                    onRemove={onRemove}
                ></Cart>
            </div>

            <div>
                <Pagination
                    nPages={nPages}
                    currentPage={currentPage}
                    setCurrentPage={setCurrentPage}
                />
            </div>


        </div>
    );
}


export default ShopPage;