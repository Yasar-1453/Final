import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
import { NavLink } from 'react-router-dom';

let DBurl = "http://localhost:5156/api/Auth/Register"

function Register() {
    let navigate = useNavigate()

    const formik = useFormik({
        initialValues: {
            name: '',
            ProfilePhoto: null,  // Мы изменили на null, так как это будет файл
            email: '',
            userName: '',
            password: '',
            confirmPassword: ''
        },
        onSubmit: async (values) => {
            console.log('Submitting values:', values);
            try {
                // Создаем новый объект FormData для отправки данных
                const formData = new FormData();

                // Добавляем все поля из формы в formData
                formData.append('name', values.name);
                formData.append('ProfilePhoto', values.ProfilePhoto);  // Файл
                formData.append('email', values.email);
                formData.append('userName', values.userName);
                formData.append('password', values.password);
                formData.append('confirmPassword', values.confirmPassword);

                // Отправляем запрос с использованием FormData
                const response = await axios.post(DBurl, formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data',  // Важный момент
                    },
                });

                if (response.status === 200 && response.data) {
                    console.log('Registration successful', response.data);
                    navigate("/");  // Перенаправление на главную страницу после успешной регистрации
                }
            } catch (error) {
                console.error('Registration failed:', error.response ? error.response.data.errors : error);
            }
        },
    });

    return (
        <div className='cont reg'>
            <form onSubmit={formik.handleSubmit} className='flex flex-col items-center gap-2 my-7'>

                <div className='flex flex-col items-start justify-between '>
                    <label htmlFor="name">Name:</label>
                    <input className='text-black' style={{ width: "300px" }}
                        required
                        id="name"
                        name="name"
                        type="text"
                        onChange={formik.handleChange}
                        value={formik.values.name}
                    />
                </div>

                <div className='flex flex-col items-start justify-between '>
                    <label htmlFor="ProfilePhoto">Profile Photo:</label>
                    <input className='text-black' style={{ width: "300px",backgroundColor:"white" }}
                        required
                        id="ProfilePhoto"
                        name="ProfilePhoto"
                        type='file'
                        onChange={(e) => formik.setFieldValue("ProfilePhoto", e.currentTarget.files[0])} // Обработчик для файла
                    />
                </div>

                <div className='flex flex-col items-start justify-between '>
                    <label htmlFor="email">Email:</label>
                    <input className='text-black' style={{ width: "300px" }}
                        required
                        id="email"
                        name="email"
                        type="email"
                        onChange={formik.handleChange}
                        value={formik.values.email}
                    />
                </div>

                <div className='flex flex-col items-start justify-between '>
                    <label htmlFor="userName">UserName:</label>
                    <input className='text-black' style={{ width: "300px" }}
                        required
                        id="userName"
                        name="userName"
                        type="text"
                        onChange={formik.handleChange}
                        value={formik.values.userName}
                    />
                </div>

                <div className='flex flex-col items-start justify-between '>
                    <label htmlFor="password">Password:</label>
                    <input className='text-black' style={{ width: "300px" }}
                        required
                        id="password"
                        name="password"
                        type="password"
                        onChange={formik.handleChange}
                        value={formik.values.password}
                    />
                </div>
                
                <div className='flex flex-col items-start justify-between '>
                    <label htmlFor="confirmPassword">Confirm Password:</label>
                    <input className='text-black' style={{ width: "300px" }}
                        required
                        id="confirmPassword"
                        name="confirmPassword"
                        type="password"
                        onChange={formik.handleChange}
                        value={formik.values.confirmPassword}
                    />
                </div>

                <button type="submit" className='border border-white p-1'>Submit</button>

                <p>Already have an account? <NavLink to="/login" className="underline">LogIn</NavLink></p>

            </form>
        </div>
    );
}

export default Register;

