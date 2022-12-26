import 'bootstrap/dist/css/bootstrap.css';
import React, {useEffect, useState} from "react";
import api from "../api";
import Header from "../Components/Header";
import {AuthPage} from "./AuthPage"
import {Button} from "react-bootstrap";
import {Link} from "react-router-dom";

function StartPage() {
    return (
        <>
            <Header></Header>
            {localStorage.getItem("username") === null
                ? (
                    <div className="Auth-form-container">
                        <AuthPage></AuthPage>
                        <h3 className={"or"}>OR</h3>
                        <form className={"Auth-form"}>
                            <div className="Auth-form-content">
                                <h3 className="Auth-form-title">Continue without login</h3>
                                <div className="d-grid gap-2 mt-3">
                                    <Link className="btn btn-primary" to={`/category/${0}`}>
                                        View categories
                                    </Link>
                                </div>
                            </div>
                        </form>
                    </div>)
                : (
                    <div className="Auth-form-container">
                    <form className={"Auth-form"}>
                        <div className="Auth-form-content">
                            <h3 className="Auth-form-title">View shop</h3>
                            <div className="d-grid gap-2 mt-3">
                                <Link className="btn btn-primary" to={`/category/${0}`}>
                                    View categories
                                </Link>
                            </div>
                        </div>
                    </form>
                </div>)
            }


        </>
    )
}

export default StartPage;