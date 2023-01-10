import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import {Link} from "react-router-dom";

export default function Header(props) {
    const username = localStorage.getItem("username");
    
    
    function logOutHandler() {
        localStorage.removeItem("username");
        localStorage.removeItem("token");
        window.location.reload(false);
    }

    return (
        <Navbar bg="light" expand="lg">
            <Container>
                <Navbar.Brand onClick={() => {
                    window.location = "/"
                }}>Shop Electronics</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        {/*<Nav.Link href="#/">All categories</Nav.Link>*/}
                        <NavDropdown title="Categories" id="basic-nav-dropdown">
                            <NavDropdown.Item onClick={() => {
                                window.location = "../category/1"
                            }}>
                                Phones and accesories
                            </NavDropdown.Item>
                            <NavDropdown.Item onClick={() => {
                                window.location = "../category/2"
                            }}>
                                Computers
                            </NavDropdown.Item>
                            <NavDropdown.Divider/>
                            <NavDropdown.Item onClick={() => {
                                window.location = "../category/0"
                            }}>
                                All categories
                            </NavDropdown.Item>
                        </NavDropdown>
                    </Nav>

                    <Nav className="justify-content-end">

                        {username === 'admin' 
                            ? <Link to={`/adminPage`} className="btn btn-primary">Admin Panel</Link>
                            : <></>
                        }
                        
                        {/*<Link to={history.go(-1)}>*/}
                        {/*    Cart{' '}*/}
                        {/*    {props.countCartItems ? (*/}
                        {/*        <button className="badge">{props.countCartItems}</button>) : ''}*/}
                        {/*</Link>*/}
                        
                        <Nav.Link href="#/">
                            {/*<Link to={'/cart'}>*/}
                            Cart{' '}
                            {props.countCartItems ? (
                                <button className="badge">{props.countCartItems}</button>) : ''}
                            {/*</Link>*/}
                        </Nav.Link>

                        {username === null
                            ? (<Nav.Link 
                                onClick={() => {window.location = ".."}}
                            >Log in</Nav.Link>)
                            : (
                                <NavDropdown title={localStorage.getItem("username")} >
                                    <NavDropdown.Item onClick={logOutHandler}>
                                        Log Out
                                    </NavDropdown.Item>
                                    <NavDropdown.Item onClick={() => {
                                        window.location = "../orderHistory"
                                    }}>
                                        Order History
                                    </NavDropdown.Item>
                                </NavDropdown>)
                        }

                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}