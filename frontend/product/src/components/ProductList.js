import React from "react";

function ProductList({ items }) {
  if (!items.length) return <p>No products found.</p>;

  return (
    <div className="product-list">
      {items.map((p) => (
        <div key={p.id} className="product-card">
          <h3>{p.name}</h3>
          <p><strong>Brand:</strong> {p.brand}</p>
          <p><strong>Category:</strong> {p.category}</p>
          <p><strong>Price:</strong> ${p.price}</p>
          <p><strong>Stock:</strong> {p.stockQuantity}</p>
          <p><strong>SKU:</strong> {p.sku}</p>
          <p>{p.description}</p>
        </div>
      ))}
    </div>
  );
}

export default ProductList;
