import React, { useContext, useEffect, useState } from 'react'
import axios from "axios"
import { FaCirclePlus } from "react-icons/fa6";
import { favoritesContext } from '../../../context/FavoritesContext';
import Swal from 'sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
import { NavLink } from 'react-router-dom';
import Slider from 'react-slick';
import { basketContext } from '../../../context/BasketContext';
let gameUrl = "http://localhost:5156/api/Game"
let genreUrl = "http://localhost:5156/api/Genre"

function Games() {
  let [data, setData] = useState([]);
  let { basket, setBasket } = useContext(basketContext);
  let [genreData, setGenreData] = useState([]);
  let { favorites, setFavorites } = useContext(favoritesContext);
  let [isVisible, setIsVisible] = useState(false);
  let [searchQuery, setSearchQuery] = useState("")
  let [filteredData, setFilteredData] = useState(data)

  function getData() {
    axios.get(gameUrl)
      .then((res) => {
        setData(res.data);
        setFilteredData(res.data); // Изначально показываем все данные
      });
  }

  function getGenreData() {
    axios.get(genreUrl)
      .then((res) => {
        setGenreData(res.data);
      });
  }

  useEffect(() => {
    getData();
    getGenreData();
  }, []);

  const handleSearch = (event) => {
    const query = event.target.value.toLowerCase();
    setSearchQuery(query);

    const filtered = data.filter(product =>
      product.name.toLowerCase().includes(query)
    );
    setFilteredData(filtered);

    // const limitedResults = filtered.slice(0, 3);
    // setFilteredData(limitedResults);
  };

  function handleAddFavorite(product) {
    let findFavorite = favorites.find(favorite => favorite.id == product.id);
    if (findFavorite) {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "You already have this item",
        showConfirmButton: true,
        confirmButtonText: "OK",
      });
    } else {
      Swal.fire({
        position: "bottom-end",
        title: "You successfully added this game to your wishlist",
        showConfirmButton: false,
        timer: 1800,
        customClass: {
          title: 'small-title'
        }
      });
      setFavorites([...favorites, product]);
    }
  }

  const settings = {
    slidesToShow: 4,
    slidesToScroll: 4,
    infinite: false,
    arrows: true,
    dots: true,
    speed: 500,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
        },
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
        },
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
    ],
  };

  const toggleVisibility = () => {
    setIsVisible(prevState => !prevState);
  };

  function handleSortByPrice() {
    let sortedGames = data.sort((a, b) => (a.price - b.price));
    setData([...sortedGames]);
  }

  function handleSortByPriceReverse() {
    let sortedGames = data.sort((a, b) => (b.price - a.price));
    setData([...sortedGames]);
  }

  return (
    <div>



      <div className='genres'>
        <button className='genre-btn' onClick={toggleVisibility}>Genres</button>
      </div>
      {isVisible && <div className="genre-slider">
        <Slider {...settings}>
          {genreData.map((genre, index) => (
            <div key={index} className="slider-item">
              <p>{genre.name}</p>
            </div>
          ))}
        </Slider>
      </div>}


      <div className='cont games-wrapper'>
        <div className='cards'>
          {filteredData.length > 0 ? (
            filteredData.map(product => (
              <div key={product.id} className='card'>
                <div className="game-img">
                  <div className="overlay">
                    <div className='fav-icon' onClick={() => handleAddFavorite(product)}><FaCirclePlus /></div>
                  </div>
                  <img src={product.imageUrl} alt={product.name} />
                </div>
                <div className='game-info'>
                  <NavLink to={`/games/${product.id}`}><h1>{product.name}</h1></NavLink>
                  <p>{product.price}<span style={{ color: " #C0F001" }}>$</span></p>
                </div>
              </div>
            ))
          ) : (
            <p>No games found</p>
          )}
        </div>
        <div className='sort-filter'>
          <p className='mb-2 text-lg'>Filters</p>
          <div className="search-container mb-2">
            <input className='search'
              type="text"
              placeholder="Search for games..."
              value={searchQuery}
              onChange={handleSearch}
            />
          </div>

          <button className='sort-btn mb-2' onClick={handleSortByPrice}>From cheap to expensive</button>
          <button className='sort-btn mb-2' onClick={handleSortByPriceReverse}>From expensive to cheap</button>
          <button className='sort-btn mb-2' onClick={getData}>Reset</button>
        </div>
      </div>
    </div>
  );
}

export default Games;
