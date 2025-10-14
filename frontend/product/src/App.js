import logo from './logo.svg';
import './App.css';
import { useState } from 'react';
import { searchProducts } from "./api";

function App() {
  const [name, setName] = useState("");
  const [products, setProducts] = useState([]);
  const handleSearch = async () => {
    try{
      const result = await searchProducts({name});
      setProducts(result);
    }
    catch(err){
      console.error(err);
      alert("Failed to load products")
    }
  }

  return (
    <div className="App">
      <h2>Product Search</h2>
      <input value={name} onChange={e => setName(e.target.value)} placeholder="Enter product name" />
      <button onClick={handleSearch}>Search</button>

      <ul>
        {products.map(p => (
          <li key={p.id}>{p.name} - ${p.price}</li>
        ))}
      </ul>
    </div>
  );
}

export default App;
