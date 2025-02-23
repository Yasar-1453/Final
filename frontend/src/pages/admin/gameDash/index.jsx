import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { NavLink, useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Game"

function GameDashboard() {
    let navigate = useNavigate()

    // const formik = useFormik({
    //     initialValues: {
    //         imageUrl: null,
    //         name: '',
    //         description: '',
    //         price: '',
    //         genreId: '',
    //         featuresId: '',

    //     },
    //     onSubmit: async (values) => {
    //         console.log('Submitting values:', values);
    //         try {
    //             // Создаем новый объект FormData для отправки данных
    //             const formData = new FormData();

    //             // Добавляем все поля из формы в formData
    //             formData.append('name', values.name);
    //             formData.append('imageUrl', values.imageUrl);  // Файл
    //             formData.append('description', values.description);
    //             formData.append('price', values.price);
    //             formData.append('price', values.genreId);
    //             formData.append('price', values.featuresId);


    //             // Отправляем запрос с использованием FormData
    //             const response = await axios.post(DBurl, formData, {
    //                 headers: {
    //                     'Content-Type': 'multipart/form-data',  // Важный момент
    //                 },
    //             });

    //             if (response.status === 200 && response.data) {
    //                 console.log('Registration successful', response.data);
    //                 // navigate("/");  // Перенаправление на главную страницу после успешной регистрации
    //             }
    //         } catch (error) {
    //             console.error('addGame failed:', error.response ? error.response.data.errors : error);
    //         }
    //     },
    // })

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
        <div className='flex bg-white'>
            <div className='admin-left' style={{ backgroundColor: "white", width: "20%", margin: "0 auto 0 0" }}>

            </div>
            <div className='admin-right text-black' style={{ backgroundColor: "white", paddingTop: "130px", width: "80%", margin: "0 0 0 auto", height: "100vh" }}>

                {/* <form onSubmit={formik.handleSubmit} className='cont add-form'>

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
                            type="file"
                            onChange={(e) => formik.setFieldValue("imageUrl", e.currentTarget.files[0])}
                    
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

                    <div className='input-wrapper'>
                        <label htmlFor="genreId">genreId</label>
                        <input className='text-black'
                            required
                            id="genreId"
                            name="genreId"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.genreId}
                        />
                    </div>

                    <div className='input-wrapper'>
                        <label htmlFor="featuresId">featuresId</label>
                        <input className='text-black'
                            required
                            id="featuresId"
                            name="featuresId"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.featuresId}
                        />
                    </div>

                    <button type="submit" className='submit-btn'>Submit</button>
                </form> */}
                <h1 className='text-center font-bold'>Games Table</h1>
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
                        <tr>
                            <NavLink to="/admin/gameDashboard/addGame"><button className="admin-btn">Add Game</button></NavLink>
                        </tr>
                    </tbody>
                </table>

            </div>
        </div>
    )
}

export default GameDashboard
