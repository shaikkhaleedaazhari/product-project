#!/bin/bash

# Example value — override as needed
API_URL=${API_URL:-http://localhost:7001/api/products}

# Generate env.js
cat <<EOF > env.js
window.ENV = {
  API_URL: "${API_URL}"
};
EOF

echo "✅ env.js generated with API_URL=${API_URL}"
