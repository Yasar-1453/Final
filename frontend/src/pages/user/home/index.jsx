import React, { useEffect, useState } from 'react'
import axios from "axios";
import Slider from "react-slick";
import { GiNestedHearts } from "react-icons/gi";
import { NavLink } from 'react-router-dom';
const gameUrl = "http://localhost:5156/api/Game";

function Home() {
  let [data, setData] = useState([]);

  function getData() {
    axios.get(gameUrl).then((res) => {
      setData(res.data);
    });
  }

  useEffect(() => {
    getData();
  }, []);



  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 4000,

  };



  return (
    <>

      <div className='slider-container'>
        {data.length == 0 ? (
          <div className='slider'>
            <img src='https://img.freepik.com/premium-photo/dark-background-stage-copy-space-colorful-neon-green-lights-bright-reflections-3d-render_334678-431.jpg?semt=ais_hybrid' alt="Extra Image" />
            <div className="wlcm">
              <p>Welcome</p>
              <p className='flex items-center gap-1'>To Our Store <GiNestedHearts /></p>
            </div>
          </div>) : (<Slider {...settings}>
            <div className='slider'>
              <img src='https://img.freepik.com/premium-photo/dark-background-stage-copy-space-colorful-neon-green-lights-bright-reflections-3d-render_334678-431.jpg?semt=ais_hybrid' alt="Extra Image" />
              <div className="wlcm">
                <p>Welcome</p>
                <p className='flex items-center gap-1'>To Our Store <GiNestedHearts /></p>
              </div>
            </div>
            {data.map((game, index) => (
              <div key={index} className='slider'>
                <img src={game.imageUrl} alt={`Game ${index}`} />
                <NavLink to={`/games/${game.id}`}><button className='slider-btn'>Play Now</button></NavLink>
              </div>
            ))}
          </Slider>)}

      </div>
    </>
  )
}

export default Home
