import {BrowserRouter, Route, Routes} from "react-router-dom";

import ProductPage from "./Pages/ProductPage";
import ShoppingCart from "./Pages/ShoppingCart";
import StartPage from "./Pages/StartPage";
import Categories from "./Pages/Categories";
import ShopPage from "./Pages/ShopPage";

export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<StartPage/>}/>
                <Route path="/category/:id" element={<Categories/>}/>
                <Route path="/shop/:id" element={<ShopPage/>}/>
                <Route path="/products/:id" element={<ProductPage/>}/>
                <Route path="/cart" element={<ShoppingCart/>}/>
                {/*<Route path="/authorization" element={<AuthorizationForm/>}/>*/}
            </Routes>
        </BrowserRouter>
    );
}
