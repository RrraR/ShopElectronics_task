import React, {useEffect, useState} from "react";
import api from "../api";
import Header from "../Components/Header";
import {Link, useParams} from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.css';
import {Button} from "react-bootstrap";

function Categories() {
    const [categories, setCategories] = useState([]);
    const {id} = useParams();

    useEffect(() => {
        api.get(`/Categories/${id}`)
            .then(function (response) {
                setCategories(response.data);
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .finally(function () {
                // always executed
            });
    }, [])

    const renderRow = category => (
        <div className={"category"} key={category.id}>
            <img className="small" src={category.imageUrl} alt={category.name}/>
            <h5>{category.name}</h5>
            <Link className="btn btn-outline-primary" to={`/shop/${category.id}`}>Show products</Link>
        </div>
    )
    return (
        <>
            <Header></Header>
            <main className={"categories"}>
                {categories.map(renderRow)}
            </main>
        </>
    )
}

export default Categories;