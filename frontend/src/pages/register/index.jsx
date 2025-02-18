
import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Auth/Register"
import { NavLink } from 'react-router-dom';
function Register() {
    let navigate = useNavigate()

    const formik = useFormik({
        initialValues: {
            name: '',
            email:'',
            userName: '',
            password: '',
            cofirmPassword:''


        },
        onSubmit: async values => {
            console.log('Submitting values:', values);
            try {
                const response = await axios.post(DBurl, values, {
                    headers: {
                      'Content-Type': 'application/json',
                    },
                  });
                  
        if (response.status === 200 && response.data) {

            console.log('Login successful', response.data);  
            navigate("/");  
          }           
            } catch (error) {
                
            }
          
        },
    })




    return (
        <>
            <div className='cont reg'>
                
                <form onSubmit={formik.handleSubmit} className='flex flex-col items-center gap-2 my-7'>

                    <div className='flex flex-col items-start justify-between '>
                        <label htmlFor="name">Name:</label>
                        <input className='text-black'
                            required
                            id="name"
                            name="name"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.name}
                        />
                    </div>

                    <div className='flex flex-col items-start justify-between '>
                        <label htmlFor="email">Email:</label>
                        <input className='text-black'
                            required
                            id="email"
                            name="email"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.email}
                        />
                    </div>

                    <div className='flex flex-col items-start justify-between '>
                        <label htmlFor="userName">UserName:</label>
                        <input className='text-black'
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
                        <input className='text-black'
                            required
                            id="password"
                            name="password"
                            type="password"
                            onChange={formik.handleChange}
                            value={formik.values.password}
                        />
                    </div>
                    <div className='flex flex-col items-start justify-between '>
                        <label htmlFor="cofirmPassword">Confirm Password:</label>
                        <input className='text-black'
                            required
                            id="cofirmPassword"
                            name="cofirmPassword"
                            type="cofirmPassword"
                            onChange={formik.handleChange}
                            value={formik.values.cofirmPassword}
                        />
                    </div>



                    <button type="submit" className='border border-white p-1'>Submit</button>

                    <p>Already have an account? <NavLink to="/login" className="underline">LogIn</NavLink></p>

                </form>
            </div>
        </>
    )
}

export default Register

