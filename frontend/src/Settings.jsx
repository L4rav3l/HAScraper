import axios from "axios";
import React, { useEffect, useState } from "react";

function Settings()
{

    const [prompt, setPrompt] = useState("");
    const [link, setLink] = useState("");

    const [promptLoading, setPromptLoading] = useState(false);
    const [urlLoading, setURLLoading] = useState(false);
    const [deleteLoading, setDeleteLoading] = useState(false);


    useEffect(() => {
        const handlePrompt = async () => {
            try
            {
                const response = await axios.get("http://localhost:5204/api/prompt")
                setPrompt(response.data);
            }
            catch(ex)
            {

            }
        }

        const handleURL = async () => {
            try
            {
                const response = await axios.get("http://localhost:5204/api/url")
                setLink(response.data);
            }
            catch(ex)
            {

            }
        }

        handlePrompt();
        handleURL();
    }, []);

    const handleURLUpdate = async () => {
        try
        {
            setURLLoading(true);

            const response = await axios.post("http://localhost:5204/api/url", {Url : link});
        }
        catch(ex)
        {

        }
        finally
        {
            setURLLoading(false);
        }
    }

    const handlePromptUpdate = async () => {
        try
        {
            setPromptLoading(true);

            const response = await axios.post("http://localhost:5204/api/prompt", {Prompt : prompt});
        }
        catch(ex)
        {

        }
        finally
        {
            setPromptLoading(false);
        }
    }

    const handleDelete = async () => {
        try
        {
            const response = await axios.delete("http://localhost:5204/api/products");
        }
        catch(ex)
        {

        }
    }

    return (
        <div class="flex flex-col gap-4 justify-center items-center">

            <div class="flex flex-col bg-white/10 w-[600px] p-2 rounded-lg gap-2">
                <span class="text-xl text-white font-semibold text-center">Change URL</span>
                <input class="w-auto h-8 rounded-lg p-2"  value={link} onChange={(e) => (setLink(e.target.value))}/>
                <button class="bg-white/10 p-2 text-white rounded-lg hover:[transform:scale(1.01)]" disabled={urlLoading} onClick={() => {handleURLUpdate()}}>Change</button>
            </div>
        
            <div class="flex flex-col bg-white/10 w-[600px] p-2 rounded-lg gap-2">
                <span class="text-xl text-white font-semibold text-center">Change Prompts</span>
                <textarea class="w-auto h-64 rounded-lg p-2" value={prompt} onChange={(e) => {setPrompt(e.target.value)}}/>
                <button class="bg-white/10 p-2 text-white rounded-lg hover:[transform:scale(1.01)]" disabled={promptLoading} onClick={() => {handlePromptUpdate()}}>Change</button>
            </div>

            <div class="flex flex-col bg-white/10 w-[600px] p-2 rounded-lg gap-2">
                <button class="bg-red-500 p-2 text-white rounded-lg hover:[transform:scale(1.01)]" onClick={() => {handleDelete()}}>DELETE ALL PRODUCTS</button>
            </div>
        
        </div>
    );
}

export default Settings;