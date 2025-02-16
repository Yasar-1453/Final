import React, { useEffect, useState } from 'react'
import { useParams, useNavigate } from "react-router-dom"
import axios from "axios"
import Slider from "react-slick";


function Details() {
  let [product, setProduct] = useState({})
  let { id } = useParams()
  let navigate = useNavigate()

  function getDetails() {
    axios.get(`http://localhost:5156/api/Game/${id}`)
      .then(res => {
        setProduct(res.data)
      })
  }

  useEffect(() => {
    getDetails()
  }, [id])

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
    <div className='cont'>
              <p>{product.name}</p>
      <div className='game-slider'>

        <Slider {...settings}>
          <div className=''>
            <img src={product.imageUrl} alt="Extra Image" className='game-slide'/>
          </div>
          {Array.isArray(product.imageUrls) && product.imageUrls.map((url, index) => (
            <img key={index} src={url} alt={`Product image ${index + 1}`} className='game-slide'/>
          ))}

        </Slider>

        <p>{product.description}</p>
        <p>Price:{product.price}$</p>
        <p>Release Date:
        {product.releaseDate}</p>
      </div>
      {/* <div>
        <img src={product.imageUrl} alt="" />
        {Array.isArray(product.imageUrls) && product.imageUrls.map((url, index) => (
          <img key={index} src={url} alt={`Product image ${index + 1}`} />
        ))}
        <p>{product.name}</p>
        <p>{product.description}</p>
        <p>{product.price}$</p>
        <p>{product.releaseDate}</p>
      </div> */}
    </div>
  )
}

export default Details
