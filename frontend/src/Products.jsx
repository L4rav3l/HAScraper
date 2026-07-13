import React from "react";
import { useNavigate } from "react-router-dom";
import { PiMemoryFill } from "react-icons/pi";
import { BsCpuFill } from "react-icons/bs";
import { RiUDiskFill } from "react-icons/ri";

function ProductList({ products, minPrice, maxPrice, minMemory, maxMemory}) {

    const formatSpec = (value, unit = "") => {
        if (value === null || value === undefined) return "None";
        if (value === 0) return "Unknown";
        return `${value}${unit}`;
    };

    const navigate = useNavigate();

    return (   
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 min-h-screen">
            {products
                .filter(product => {
                    const priceOk = !minPrice || product.price >= Number(minPrice);
                    const maxPriceOk = !maxPrice || product.price <= Number(maxPrice);
                    const memoryOk = !minMemory || product.memory >= Number(minMemory);
                    const maxMemoryOk = !maxMemory || product.memory <= Number(maxMemory);
                    return priceOk && maxPriceOk && memoryOk && maxMemoryOk;
                }).sort((a, b) => a.price - b.price)
            .map((product, index) => (
                
                
                <div 
                    key={index} 
                    class={`flex flex-col justify-between p-6 rounded-2xl shadow-sm transition-all duration-300 hover:shadow-md backdrop-blur-md hover:[transform:scale(1.05)] cursor-pointer w-96 h-64 ${
                        product.frozen === true 
                            ? "bg-cyan-400/10" 
                            : "bg-white/10"
                    }`}
                    onClick={() => window.location.href = product.url}
                >
                    

                    <div class="space-y-3">
                        <div class="flex items-center justify-between border-gray-100/50 pb-2">
                            <span class="flex flex-rows justify-center items-center text-sm font-medium text-gray-200 gap-2"><BsCpuFill size={18}/>Processor</span>
                            <span class="text-sm font-bold text-gray-300 py-0.5 rounded">
                                {formatSpec(product.cpu)}
                            </span>
                        </div>

                        <div class="flex items-center justify-between border-gray-100/50 pb-2">
                            <span class="flex flex-rows justify-center items-center text-sm font-medium text-gray-300 gap-2"><PiMemoryFill size={18}/> Memory</span>
                            <span class="text-sm font-semibold text-gray-300">
                                {formatSpec(product.memory, " GB")} 
                                <span class="text-xs text-gray-300 ml-1">
                                    ({formatSpec(product.ddr)})
                                </span>
                            </span>
                        </div>

                        <div class="flex items-center justify-between pb-2">
                            <span class="flex flex-rows justify-center items-center text-sm font-medium text-gray-300 gap-2"><RiUDiskFill size={18}/> Storage</span>
                            <span class="text-sm font-semibold text-gray-300">
                                {formatSpec(product.drive)}
                            </span>
                        </div>
                    </div>

                                    <div className="h-px bg-gray-300 my-4" />

                <div class="flex items-center justify-between w-full rounded-xl gap-4">
                    
                    <div class="flex items-center">
                        <span class={`text-xs font-semibold px-3 py-1.5 rounded-lg ${
                            product.frozen === true 
                                ? "bg-blue-100 text-blue-800" 
                                : "bg-green-300 text-green-800"
                        }`}>
                            {product.frozen === true ? "Frozen" : "Active"}
                        </span>
                    </div>

                    <div class="flex items-center font-semibold text-white text-2xl">
                        {product.price.toLocaleString()} Ft
                    </div>

                </div>

                </div>
                ))}
        </div>
    );
}
export default ProductList;