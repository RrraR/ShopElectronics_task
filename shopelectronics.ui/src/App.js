import {BrowserRouter, Route, Routes} from "react-router-dom";

import StartPage from "./Pages/StartPage";
import Categories from "./Pages/Categories";
import ShopPage from "./Pages/ShopPage";
import AdminPage from "./Pages/AdminPage";
import OrderHistoryPage from "./Pages/OrderHistoryPage";
// import AuthPage from "./Pages/AuthPage";

export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<StartPage/>}/>
                <Route path="/category/:id" element={<Categories/>}/>
                {/*<Route path="/auth" element={<AuthPage/>}></Route>*/}
                <Route path="/orderHistory" element={<OrderHistoryPage/>}/>
                <Route path="/adminPage" element={<AdminPage/>}/>
                <Route path="/shop/:id" element={<ShopPage/>}/>
                {/*<Route path="/authorization" element={<AuthorizationForm/>}/>*/}
            </Routes>
        </BrowserRouter>
    );
}
