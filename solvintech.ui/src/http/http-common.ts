import axios from "axios";

const http = axios.create({
    baseURL: "http://localhost:5062/api",
    headers: {
        "Content-Type": "application/json"
    }
})

export default http;