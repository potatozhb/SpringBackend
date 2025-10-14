
const API_URL = process.env.REACT_APP_API_URL;


export async function searchProducts(filter)
{
    const query = new URLSearchParams(filter).toString();
    const response = await fetch(`${API_URL}/api/v1/product/search?${query}`);

    if(!response.ok){
        throw new Error(`Error: ${response.status}`);
    }

    return await response.json();
}


export async function getProducts()
{
    const response = await fetch(`${API_URL}/api/v1/product`);

    if(!response.ok){
        throw new Error(`Error: ${response.status}`);
    }

    return await response.json();
}