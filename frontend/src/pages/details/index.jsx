import React, { useEffect, useState } from 'react'
import { useParams, useNavigate } from "react-router-dom"
import axios from "axios"

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
  return (
    <>
      <div>
        <img src={product.imageUrl} alt="" />
        <p>{product.name}</p>
        <p>{product.description}</p>
        <p>{product.price}$</p>
        <p>{product.releaseDate}</p>
      </div>
    </>
  )
}

export default Details
