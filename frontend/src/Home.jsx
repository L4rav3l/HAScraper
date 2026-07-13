import axios from "axios";
import React, { useEffect } from "react";
import { useState } from "react";
import ProductList from "./Products";
import { RiMoneyEuroBoxFill } from "react-icons/ri";

function Home()
{

    const [product, setProduct] = useState([]);
    const [loading, setLoading] = useState(false);

    const [minPrice, setMinPrice] = useState();
    const [maxPrice, setMaxPrice] = useState();

    const [minMemory, setMinMemory] = useState(4);
    const [maxMemory, setMaxMemory] = useState(128);

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                setLoading(true);
                const response = await axios.get("http://localhost:5204/api/products");
                    
                setProduct(response.data);
            } catch (err) {
                console.log(err);
            } finally {
                setLoading(false);
            }
        }

        fetchProducts();
    }, []);

    return (
        <>
            <div class="flex justify-center items-center gap-4">
                <div class="bg-white/10 backdrop-blur p-2 rounded-lg flex flex-row w-64 gap-2">
                    <div class="flex flex-col">
                        <span class="flex justify-center text-white">Min Price</span>
                        <input class="rounded-lg w-full bg-white/10 backdrop-blur text-white text-center placeholder:text-gray-300 placeholder:text-center" onChange={(e) => (setMinPrice(e.target.value))} value={minPrice} placeholder="100,000 Ft"/>
                    </div>
                    <span class="text-white">-</span>
                    <div class="flex flex-col">
                        <span class="flex justify-center text-white">Max Price</span>
                        <input class="rounded-lg w-full bg-white/10 backdrop-blur text-white text-center placeholder:text-gray-300 placeholder:text-center" onChange={(e) => (setMaxPrice(e.target.value))} value={maxPrice} placeholder="150,000 Ft" />
                    </div>
                </div>

                <div class="bg-white/10 backdrop-blur p-2 rounded-lg flex flex-row w-64 gap-2">
                    <div class="flex flex-col">
                        <span class="flex justify-center text-white">Min Memory</span>
                        <input class="rounded-lg w-full bg-white/10 backdrop-blur text-white text-center placeholder:text-gray-300 placeholder:text-center" onChange={(e) => {const value = e.target.value.replace(/[^0-9]/g, ""); setMinMemory(value)}} value={minMemory + " GB"} placeholder="4 GB"/>
                    </div>
                    <span class="text-white">-</span>
                    <div class="flex flex-col">
                        <span class="flex justify-center text-white">Max Memory</span>
                        <input class="rounded-lg w-full bg-white/10 backdrop-blur text-white text-center placeholder:text-gray-300 placeholder:text-center" onChange={(e) => {const value = e.target.value.replace(/[^0-9]/g, ""); setMaxMemory(value)}} value={maxMemory + " GB"} placeholder="16 GB"/>
                    </div>
                </div>
            </div>
            <div class="flex flex-col justify-center items-center p-8 gap-4">
                <ProductList products={product} minPrice={minPrice} maxPrice={maxPrice} minMemory={minMemory} maxMemory={maxMemory}/>
            </div>
        </>
    );
}

export default Home;