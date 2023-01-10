import React, {useEffect, useState} from "react";
import api from "../api";
import Header from "../Components/Header";
import ProductList from "../Components/ProductList"
import Cart from "../Components/Cart";
import {useParams} from "react-router-dom";
import {usePrevious} from "../utils";
import Pagination from "../Components/Pagination";
import {Form} from "react-bootstrap";

function ShopPage() {
    const {id} = useParams();
    const [products, setProducts] = useState([]);
    const [searchProductList, setSearchProductList] = useState([]);
    const [cartItems, setCartItems] = useState([]);
    const username = localStorage.getItem("username");

    const [searchQuery, setSearchQuery] = useState('');
    const [filterQuery, setFilterQuery] = useState('');
    
    const [currentPage, setCurrentPage] = useState(1);
    const [recordsPerPage] = useState(2);
    const indexOfLastRecord = currentPage * recordsPerPage;
    const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
    const currentRecords = products.slice(indexOfFirstRecord,
        indexOfLastRecord);
    const nPages = Math.ceil(products.length / recordsPerPage)
    // let brands = [];
    

    useEffect(() => {
        api.get(`Product/category/${id}/products`)
            .then(function (response) {
                setProducts(response.data);
                setSearchProductList(response.data);
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .finally(function () {
                // always executed
            });
        // getProducts();

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
    
    // useEffect(()=>{
    //     brands = [...new Set(searchProductList.flatMap(({brandName}) => brandName))]
    //     console.log(brands)
    // }, [searchProductList])
    

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

    function search(productsToSearch) {
        const search_parameters = ["name", "description", "brandName"];
        return productsToSearch.filter(
            (product) =>
                search_parameters.some((parameter) =>
                    product[parameter].toString().toLowerCase().includes(searchQuery))
        );
    }

    useEffect(()=>
    {
        const data = Object.values(searchProductList)
        setProducts(search(data))
    }, [searchQuery])
    
    
    useEffect(()=>
    {
        const data = Object.values(searchProductList)
        setProducts(filter(data))
    }, [filterQuery])
    
    


    function filter(productsToFilter) {
        const filter_parameters = ["brandName"];
        return productsToFilter.filter(
            (product) =>
                filter_parameters.some((parameter) =>
                    product[parameter].toString().toLowerCase().includes(filterQuery))
        );
    }

    return (
        <div className="App">
            <Header countCartItems={cartItems.length}></Header>

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
                    {searchProductList.map(
                        c => (
                            <option key={c.productId} value={c.brandName.toString().toLowerCase()}>{c.brandName.toString().toLowerCase()}</option>))
                    }
                </Form.Select>


            </div>

            <div className="mainRow">

                <ProductList
                    products={
                        currentRecords
                    }
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