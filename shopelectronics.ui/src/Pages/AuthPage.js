import React, {useState} from "react"
import api from "../api";
import {Button, Form} from "react-bootstrap";

export const AuthPage = (props) => {
    let [authMode, setAuthMode] = useState("login")
    const [userData, setUserData] = useState([]);
    const [usernameInput, setUsernameInput] = useState('');
    const [passwordInput, setPasswordInput] = useState('');


    const changeAuthMode = () => {
        setAuthMode(authMode === "login" ? "register" : "login")
    }

    function registrationHandler() {

    }

    function loginHandler(event) {
        event.preventDefault();
        api.post(`Account/login`, 
            {
            username: usernameInput,
            password: passwordInput
        }).then(
            response => response.status === 200 ? setData(response.data) : console.log(response)
                //setUserData(response.data)
            //r => r.status === 200 ? setUserData(r.data) : console.log(r)
        )
        // if (userData.username !== undefined) {
        //     setData()
        // }
    }

    function setData(data) {
        setUserData(data);
        localStorage.setItem("username", data.username)
        localStorage.setItem("token", data.accessToken)

        // console.log(localStorage.getItem("username"))
        // console.log(localStorage.getItem("token"))

        setUsernameInput('');
        setPasswordInput('');
    }


    // const passwordInputHandle = event => {
    //     setPasswordInput(event.target.value)
    // }
    //
    // const usernameInputHandle = event => {
    //     setUsernameInput(event.target.value)
    // }

    if (authMode === "login") {
        return (
            // <div className="Auth-form-container">
            <Form onSubmit={loginHandler} className="Auth-form">
                <div className="Auth-form-content">
                    <h3 className="Auth-form-title">Login</h3>
                    <div className="text-center">
                        Not registered yet?{" "}
                        <span className="link-primary" onClick={changeAuthMode}>Register</span>
                    </div>
                    <div className="form-group mt-3">
                        <label>Username</label>
                        <input
                            type="text"
                            className="form-control mt-1"
                            placeholder="Enter username"
                            onChange={(event) => setUsernameInput(event.target.value)}
                            value={usernameInput}
                        />
                    </div>
                    <div className="form-group mt-3">
                        <label>Password</label>
                        <input
                            type="password"
                            className="form-control mt-1"
                            placeholder="Enter password"
                            onChange={(event) => setPasswordInput(event.target.value)}
                            value={passwordInput}
                        />
                    </div>
                    <div className="d-grid gap-2 mt-3">
                        <Button type="submit" className="btn btn-primary">
                            Submit
                        </Button>
                    </div>
                </div>
            </Form>
            // </div>
        )
    }

    return (
        // <div className="Auth-form-container">
        <form className="Auth-form">
            <div className="Auth-form-content">
                <h3 className="Auth-form-title">Register</h3>
                <div className="text-center">
                    Already registered?{" "}
                    <span className="link-primary" onClick={changeAuthMode}>Login</span>
                </div>
                <div className="form-group mt-3">
                    <label>Username</label>
                    <input
                        type="text"
                        className="form-control mt-1"
                        placeholder="e.g JaneDoe"
                        onChange={(e) => {
                            setUsernameInput(e.target.value)
                        }}
                    />
                </div>
                {/*<div className="form-group mt-3">*/}
                {/*    <label>Email address</label>*/}
                {/*    <input*/}
                {/*        type="email"*/}
                {/*        className="form-control mt-1"*/}
                {/*        placeholder="Email Address"*/}
                {/*    />*/}
                {/*</div>*/}
                <div className="form-group mt-3">
                    <label>Password</label>
                    <input
                        type="password"
                        className="form-control mt-1"
                        placeholder="Password"
                        onChange={(e) => {
                            setPasswordInput(e.target.value)
                        }}
                    />
                </div>
                <div className="d-grid gap-2 mt-3">
                    <button type="submit" onClick={registrationHandler} className="btn btn-primary">
                        Submit
                    </button>
                </div>
            </div>
        </form>
        // </div>
    )
}