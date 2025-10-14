import React, { useState } from "react";

function ProductSearchForm({ onSearch }) {
  const [filters, setFilters] = useState({
    name: "",
    category: "",
    brand: "",
    minPrice: "",
    maxPrice: "",
    minStock: "",
    maxStock: ""
  });

  const handleChange = (e) => setFilters({ ...filters, [e.target.name]: e.target.value });
  const handleSubmit = (e) => {
    e.preventDefault();
    onSearch(filters);
  };

  return (
    <form onSubmit={handleSubmit} className="search-form">
      <input name="name" placeholder="Name" value={filters.name} onChange={handleChange} />
      <input name="category" placeholder="Category" value={filters.category} onChange={handleChange} />
      <input name="brand" placeholder="Brand" value={filters.brand} onChange={handleChange} />
      <input type="number" name="minPrice" placeholder="Min Price" value={filters.minPrice} onChange={handleChange} />
      <input type="number" name="maxPrice" placeholder="Max Price" value={filters.maxPrice} onChange={handleChange} />
      <input type="number" name="minStock" placeholder="Min Stock" value={filters.minStock} onChange={handleChange} />
      <input type="number" name="maxStock" placeholder="Max Stock" value={filters.maxStock} onChange={handleChange} />
      <button type="submit">Search</button>
    </form>
  );
}

export default ProductSearchForm;