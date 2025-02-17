
import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Auth/Login"
import { NavLink } from 'react-router-dom';
function LogIn() {
  let navigate = useNavigate()

  const formik = useFormik({
    initialValues: {
      Username: '',
      Password: ''


    },
    onSubmit: async (values) => {
      if (!values.Username || !values.Password) {
        alert('Both username and password are required!');
        return;
      }
      console.log('Submitting values:', values);

      try {
        // Отправляем запрос на сервер
        const response = await axios.post(DBurl, values, {
          headers: {
            'Content-Type': 'application/json',
          },
        });
        // Проверяем ответ от сервера
        if (response.status === 200 && response.data) {
          // Если авторизация прошла успешно, перенаправляем на другую страницу
          console.log('Login successful', response.data);  // Выводим данные о пользователе (или токен)
          navigate("/");  // Переход на главную страницу
        } else {
          alert("Invalid login credentials!");  // Ошибка: неверные данные
        }
      } catch (error) {
        console.error('Login failed:', error.response ? error.response.data.errors : error);
        alert("An error occurred while logging in. Please try again.");
      }
    },
  })

  
  return (
    <>
      <div className='cont'>
        <form onSubmit={formik.handleSubmit} className='flex flex-col items-start gap-2 my-7'>

        <div className='flex flex-col items-start justify-between '>
                        <label htmlFor="Username">UserName:</label>
                        <input className='text-black'
                            required
                            id="Username"
                            name="Username"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.Username}
                        />
                    </div>

          <div className='flex flex-col items-start justify-between '>
            <label htmlFor="Password">Password:</label>
            <input className='text-black'
              required
              id="Password"
              name="Password"
              type="password"
              onChange={formik.handleChange}
              value={formik.values.Password}
            />
          </div>



          <button type="submit" className='border border-black p-1'>Submit</button>

          <p>Dont have an account? <NavLink to="/register">Register</NavLink></p>

        </form>
      </div>
    </>
  )
}

export default LogIn
