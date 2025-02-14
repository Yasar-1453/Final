import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import FavoritesProvider from './context/FavoritesContext.jsx'
import BasketProvider from './context/BasketContext.jsx'

createRoot(document.getElementById('root')).render(
  <FavoritesProvider>
    <BasketProvider>
      <App />
    </BasketProvider>
  </FavoritesProvider>,
)
