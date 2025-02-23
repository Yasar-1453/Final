import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Game"

function GameDashboard() {
    let navigate = useNavigate()

    const formik = useFormik({
        initialValues: {
            imageUrl: '',
            name: '',
            description: '',
            price: ''

        },
        onSubmit: values => {
            axios.post(
                DBurl, values)
        },
    })

    let [data, setData] = useState([])

    function getData() {
        axios.get(DBurl)
            .then(res => {
                setData(res.data)

            })
    }

    useEffect(() => {
        getData()
    }, [])

    function deleteData(id) {
        let dataId = data.filter(el => el.id !== id)
        setData(dataId)

        axios.delete(`${DBurl}/${id}`)
            .then(() => {
                getData()
            })
    }
    return (
        <div className='flex'>
            <div style={{ backgroundColor: "white", width: "20%",margin: "0 auto 0 0" }}>

            </div>
            <div style={{ backgroundColor: "gray", paddingTop: "20px", width: "80%", margin: "0 0 0 auto",height:"100vh" }}>

                <form onSubmit={formik.handleSubmit} className='cont add-form'>

                    <div className='input-wrapper'>
                        <label htmlFor="name">Name</label>
                        <input className='text-black'
                            required
                            id="name"
                            name="name"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.name}
                        />
                    </div>

                    <div className='input-wrapper'>
                        <label htmlFor="imageUrl">Image</label>
                        <input className='text-black'
                            required
                            id="imageUrl"
                            name="imageUrl"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.imageUrl}
                        />
                    </div>
                    <div>
                        <label htmlFor="price">Price</label>
                        <input className='text-black'
                            id="price"
                            name="price"
                            type="number"
                            onChange={formik.handleChange}
                            value={formik.values.price}
                        />
                    </div>
                    <div className='input-wrapper'>
                        <label htmlFor="description">Description</label>
                        <input className='text-black'
                            required
                            id="description"
                            name="description"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.description}
                        />
                    </div>

                    <button type="submit" onClick={() => navigate("/games")} className='submit-btn'>Submit</button>
                </form>

                <table className='cont' style={{ marginTop: "50px" }}>
                    <thead>
                        <tr style={{ textAlign: "left" }}>
                            <th>Image</th>
                            <th>Name</th>
                            <th>Description</th>
                            <th>Price</th>
                            <th>Delete</th>

                        </tr>
                    </thead>
                    <tbody>
                        {
                            data && data.map(product => (
                                <tr key={product.id}>
                                    <td><img src={product.imageUrl} alt="" style={{ width: "80px", height: "80px", objectFit: "contain" }} /></td>
                                    <td>{product.name}</td>
                                    <td>{product.description}</td>
                                    <td>{product.price}</td>
                                    <td><button className='delete-btn' onClick={() => deleteData(product.id)}>Delete</button></td>
                                </tr>
                            ))
                        }
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default GameDashboard
