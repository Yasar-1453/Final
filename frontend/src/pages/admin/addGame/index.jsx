import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Game"

function AddGame() {
    let navigate = useNavigate()

    const formik = useFormik({
        initialValues: {
            imageUrl: null,
            name: '',
            description: '',
            price: '',
            genreId: '',
            featuresId: '',

        },
        onSubmit: async (values) => {
            console.log('Submitting values:', values);
            try {
                // Создаем новый объект FormData для отправки данных
                const formData = new FormData();

                // Добавляем все поля из формы в formData
                formData.append('name', values.name);
                formData.append('Image', values.imageUrl);  // Файл
                formData.append('description', values.description);
                formData.append('price', values.price);
                formData.append('genreId', values.genreId);
                formData.append('featuresId', values.featuresId);


                // Отправляем запрос с использованием FormData
                const response = await axios.post(DBurl, formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data',  // Важный момент
                    },
                });

                if (response.status === 200 && response.data) {
                    console.log('Registration successful', response.data);
                    // navigate("/");  // Перенаправление на главную страницу после успешной регистрации
                }
            } catch (error) {
                console.error('addGame failed:', error.response ? error.response.data.errors : error);
            }
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
        <div className='flex bg-white'>
            <div className='admin-left' style={{ backgroundColor: "white", width: "20%", margin: "0 auto 0 0" }}>

            </div>
            <div className='admin-right text-black' style={{ backgroundColor: "white", paddingTop: "130px", width: "80%", margin: "0 0 0 auto", height: "100vh" }}>
                <form onSubmit={formik.handleSubmit} className='cont add-form'>

                    <div className='flex flex-col items-start justify-between'>
                        <label htmlFor="name">Name:</label>
                        <input className='text-black border border-black'
                            required
                            id="name"
                            name="name"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.name}
                        />
                    </div>

                    <div className='flex flex-col items-start justify-between'>
                        <label htmlFor="imageUrl">Image:</label>
                        <input className='text-black'
                            required
                            id="imageUrl"
                            name="imageUrl"
                            type="file"
                            onChange={(e) => formik.setFieldValue("imageUrl", e.currentTarget.files[0])}

                        />
                    </div>
                    <div className='flex flex-col items-start justify-between'>
                        <label htmlFor="price">Price:</label>
                        <input className='text-black border border-black'
                            id="price"
                            name="price"
                            type="number"
                            onChange={formik.handleChange}
                            value={formik.values.price}
                        />
                    </div>
                    <div className='flex flex-col items-start justify-between'>
                        <label htmlFor="description">Description:</label>
                        <input className='text-black border border-black'
                            required
                            id="description"
                            name="description"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.description}
                        />
                    </div>

                    <div className='flex flex-col items-start justify-between'>
                        <label htmlFor="genreId">genreId:</label>
                        <input className='text-black border border-black'
                            required
                            id="genreId"
                            name="genreId"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.genreId}
                        />
                    </div>

                    <div className='flex flex-col items-start justify-between'>
                        <label htmlFor="featuresId">featuresId:</label>
                        <input className='text-black border border-black'
                            required
                            id="featuresId"
                            name="featuresId"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.featuresId}
                        />
                    </div>

                    <button type="submit" className='mt-2 admin-btn'>Submit</button>
                </form>
            </div>
        </div>
    )
}

export default AddGame
