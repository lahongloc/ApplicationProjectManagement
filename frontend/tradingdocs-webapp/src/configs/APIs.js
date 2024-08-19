import axios from "axios";
import cookie from "react-cookies";

export const BASE_URL = "http://localhost:3000";

export const endpoints = {
	login: "/user/login",
	
	register: "/user/register",
	
	signOut: "/user/logout",
	
};

export default axios.create({
	baseURL: BASE_URL,
});
