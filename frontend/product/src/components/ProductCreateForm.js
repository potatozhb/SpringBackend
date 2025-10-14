import React, { useState } from "react";

function ProductCreateForm({ onCreate }) {
  const [product, setProduct] = useState({
    name: "",
    category: "",
    brand: "",
    price: "",
    stockQuantity: "",
    sku: "",
    description: ""
  });

  const handleChange = (e) => setProduct({ ...product, [e.target.name]: e.target.value });
  const handleSubmit = (e) => {
    e.preventDefault();
    onCreate(product);
  };

  return (
    <form onSubmit={handleSubmit} className="create-form">
      <input name="name" placeholder="Name" value={product.name} onChange={handleChange} required />
      <input name="category" placeholder="Category" value={product.category} onChange={handleChange} required />
      <input name="brand" placeholder="Brand" value={product.brand} onChange={handleChange} required />
      <input type="number" name="price" placeholder="Price" value={product.price} onChange={handleChange} required />
      <input type="number" name="stockQuantity" placeholder="Stock Quantity" value={product.stockQuantity} onChange={handleChange} required />
      <input name="sku" placeholder="SKU" value={product.sku} onChange={handleChange} required />
      <textarea name="description" placeholder="Description" value={product.description} onChange={handleChange} />
      <button type="submit">Create Product</button>
    </form>
  );
}

export default ProductCreateForm;
