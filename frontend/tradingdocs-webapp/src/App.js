import { createContext, useEffect, useReducer } from "react";
import cookie from "react-cookies";
import "./App.css";
import UserReducer from "./reducers/UserReducer";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import HomePage from "./components/site-compos/HomePage";

import AppAppBar from "./components/site-compos/AppAppBar";

export const UserContext = createContext();

function App() {
	return (
		<BrowserRouter>
			<AppAppBar />

			<Routes>
				<Route path="/" element={<HomePage />} />
				{isNormalUser(user) && (
					<>
						<Route
							path="/upload-documents"
							element={<UploadDocument />}
						/>
						<Route
							path="/update-document"
							element={<UpdateDocument />}
						/>
					</>
				)}
			</Routes>
		</BrowserRouter>
	);
}

export default App;
