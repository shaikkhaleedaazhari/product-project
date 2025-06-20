// API URL - This should point to your EC2 backend IP and port
const API_URL = 'http://products-backend.default.svc.cluster.local/api/products';

// DOM Elements
const productsContainer = document.getElementById('products-container');
const addProductForm = document.getElementById('add-product-form');
const productTemplate = document.getElementById('product-template');

// Fetch all products
async function fetchProducts() {
    try {
        const response = await fetch(API_URL);
        
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        
        const products = await response.json();
        displayProducts(products);
    } catch (error) {
        console.error('Error fetching products:', error);
        showAlert('Failed to load products. Please try again later.', 'danger');
        productsContainer.innerHTML = `
            <div class="col-12 text-center">
                <p class="text-danger">Failed to load products. Please try again later.</p>
            </div>
        `;
    }
}

// Display products in the UI
function displayProducts(products) {
    productsContainer.innerHTML = '';
    
    if (products.length === 0) {
        productsContainer.innerHTML = `
            <div class="col-12 text-center">
                <p>No products available. Add a new product to get started!</p>
            </div>
        `;
        return;
    }
    
    products.forEach(product => {
        const productElement = productTemplate.content.cloneNode(true);
        
        const imgElement = productElement.querySelector('.product-image');
        imgElement.src = product.imageUrl || 'https://via.placeholder.com/300x200?text=No+Image';
        imgElement.alt = product.name;
        
        productElement.querySelector('.product-name').textContent = product.name;
        productElement.querySelector('.product-description').textContent = product.description;
        productElement.querySelector('.product-price').textContent = product.price.toFixed(2);
        
        productsContainer.appendChild(productElement);
    });
}

// Add a new product
async function addProduct(productData) {
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(productData)
        });
        
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        
        const newProduct = await response.json();
        showAlert('Product added successfully!', 'success');
        fetchProducts(); // Refresh the product list
        return newProduct;
    } catch (error) {
        console.error('Error adding product:', error);
        showAlert('Failed to add product. Please try again.', 'danger');
        throw error;
    }
}

// Show alert message
function showAlert(message, type = 'info') {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
    alertDiv.role = 'alert';
    
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;
    
    document.body.appendChild(alertDiv);
    
    // Auto-dismiss after 5 seconds
    setTimeout(() => {
        alertDiv.classList.remove('show');
        setTimeout(() => alertDiv.remove(), 300);
    }, 5000);
}

// Event Listeners
document.addEventListener('DOMContentLoaded', () => {
    // Load products when page loads
    fetchProducts();
    
    // Handle form submission
    addProductForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        
        const productData = {
            name: document.getElementById('name').value,
            description: document.getElementById('description').value,
            price: parseFloat(document.getElementById('price').value),
            imageUrl: document.getElementById('imageUrl').value || 'https://via.placeholder.com/300x200?text=No+Image'
        };
        
        try {
            await addProduct(productData);
            addProductForm.reset(); // Clear the form
        } catch (error) {
            // Error is already handled in addProduct function
        }
    });
});
