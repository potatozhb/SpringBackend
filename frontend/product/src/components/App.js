import React, { useState, useEffect } from "react";
import ProductList from "./ProductList";
import ProductSearchForm from "./ProductSearchForm";
import ProductCreateForm from "./ProductCreateForm";
import { searchProducts, createProduct, getProducts } from "../APIs/api";

function App() {
  const [products, setProducts] = useState([]);
  const [error, setError] = useState("");

  // Fetch all products initially
  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const result = await getProducts();
        console.log("All products:", result);
        setProducts(result || []);
      } catch (err) {
        console.error(err);
        setError(err.message);
      }
    };

    fetchProducts();
  }, []); 

  const handleSearch = async (filters) => {
    setError("");
    try {
      const result = await searchProducts(filters);
      setProducts(result || []);
    } catch (err) {
      setError(err.message);
      setProducts([]);
    }
  };

  const handleCreate = async (product) => {
    setError("");
    try {
      await createProduct(product);
      alert("Product created!");
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <div className="app">
      <h1>Product Management</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}

      <h2>Create Product</h2>
      <ProductCreateForm onCreate={handleCreate} />

      <h2>Search Products</h2>
      <ProductSearchForm onSearch={handleSearch} />

      <ProductList items={products} />
    </div>
  );
}

export default App;