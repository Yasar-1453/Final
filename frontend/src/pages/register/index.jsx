
import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Auth/Login"
import { NavLink } from 'react-router-dom';
function Register() {
    let navigate = useNavigate()

    const formik = useFormik({
        initialValues: {
            name: '',
            email:'',
            username: '',
            password: '',
            confirmpassword:''


        },
        onSubmit: values => {
            console.log('Submitting values:', values);
            axios.post(DBurl, values)
        },
    })




    return (
        <>
            <div className='cont'>
                <form onSubmit={formik.handleSubmit} className='flex flex-col items-start gap-2 my-7'>

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
                        <label htmlFor="username">UserName:</label>
                        <input className='text-black'
                            required
                            id="username"
                            name="username"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.username}
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
                        <label htmlFor="confirmpassword">Confirm Password:</label>
                        <input className='text-black'
                            required
                            id="confirmpassword"
                            name="confirmpassword"
                            type="confirmpassword"
                            onChange={formik.handleChange}
                            value={formik.values.confirmpassword}
                        />
                    </div>



                    <button type="submit" className='border border-white p-1'>Submit</button>

                    <p>Already have an account? <NavLink to="/login">LogIn</NavLink></p>

                </form>
            </div>
        </>
    )
}

export default Register

