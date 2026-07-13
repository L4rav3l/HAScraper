import React, { useState } from "react";
import Home from "./Home";
import { GoHomeFill } from "react-icons/go";
import { RiSettings2Fill } from "react-icons/ri";
import { RiRobot2Fill } from "react-icons/ri";
import Settings from "./Settings";

function Menu() {
    const [page, setPage] = useState(0);

    return (
        <>
            <div class="flex flex-row gap-2 p-2 backdrop-blur-md">
                <div class="bg-white/10 w-max p-1 rounded-lg cursor-pointer hover:[transform:scale(1.1)]" onClick={() => setPage(0)}>
                    <GoHomeFill size={24} class="text-white"/>
                </div>
                <div class="bg-white/10 w-max p-1 rounded-lg cursor-pointer hover:[transform:scale(1.1)]" onClick={() => setPage(1)}>
                    <RiSettings2Fill size={24} class="text-white"/>
                </div>
                <div class="bg-white/10 w-max p-1 rounded-lg cursor-pointer hover:[transform:scale(1.1)]" onClick={() => setPage(2)}>
                    <RiRobot2Fill size={24} class="text-white"/>
                </div>
            </div>

            {page === 0 && <Home />}
            {page === 1 && <Settings />}
        </>
    );
}

export default Menu;