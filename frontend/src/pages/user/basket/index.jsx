import React, { useContext } from 'react'
import { useState } from 'react';
import { useEffect } from 'react';
import { basketContext } from '../../../context/BasketContext';
function Basket() {
  let { basket, setBasket } = useContext(basketContext)
  let [totalPrice, setTotalPrice] = useState(0)
  function handleDecrease(item) {
    if (item.count > 1) {
      item.count--
      setBasket([...basket])
    } else {
      let filtered = basket.filter(elem => elem.id != item.id)
      setBasket(filtered)
    }
  }

  function handleIncrease(item) {
    item.count++
    setBasket([...basket])
  }

  function handleDelete(item) {
    let filtered = basket.filter(elem => elem.id != item.id)
    setBasket(filtered)

  }


  function calcTotalPrice() {
    let total = basket.reduce((sum, item) => sum + (item.price * item.count), 0)
    setTotalPrice(total)
  }
  useEffect(() => {
    calcTotalPrice()
  }, [basket])




  return (
    <>
      {
        basket.length == 0 ? (
          <h2 style={{ textAlign: "center" }}>Your Basket is Empty</h2>
        ) : (
          <>
            <h1 style={{ textAlign: "center" }}>Your Basket</h1>
            <table style={{ width: "80%", margin: "0 auto" }}>
              <thead>
                <tr className='text-left'>
                  <th>Image</th>
                  <th>Name</th>
                  <th>Price</th>
                  <th>Total Price</th>
                  <th>Decrease</th>
                  <th>Count</th>
                  <th>Increase</th>
                  <th>Delete</th>
                </tr>
              </thead>
              <tbody>
                {
                  basket.map(item => (
                    <tr key={item.id}>
                      <td><img src={item.imageUrl} alt="" style={{ width: "80px", height: "80px", objectFit: "contain" }} /></td>
                      <td>{item.name}</td>
                      <td>{item.price}$</td>
                      <td>{(item.price * item.count).toFixed(2)}$</td>
                      <td><button onClick={() => handleDecrease(item)}>-</button></td>
                      <td>{item.count}</td>
                      <td><button onClick={() => handleIncrease(item)}>+</button></td>
                      <td><button onClick={() => handleDelete(item)}>Delete</button></td>
                    </tr>
                  ))
                }
              </tbody>

            </table>

            <div style={{width:"80%", margin:"0 auto",padding:"20px 0px"}}> Total : {totalPrice.toFixed(2)}$ </div>

          </>
        )
      }
    </>
  )
}

export default Basket