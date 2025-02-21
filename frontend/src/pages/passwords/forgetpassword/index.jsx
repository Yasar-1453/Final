import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Auth/ForgotPassword"
import { NavLink } from 'react-router-dom';

function ForgetPassword() {
    let navigate = useNavigate()

    const formik = useFormik({
        initialValues: {
            email: '',


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

                    console.log('password changing successful', response.data);

                }
            } catch (error) {
                console.error('password change failed:', error.response ? error.response.data.errors : error);
            }

        },
    })

    return (
        <>
            <div className='cont reg'>
                <form onSubmit={formik.handleSubmit} className='flex flex-col items-center gap-2 my-7'>
                    <div className='flex flex-col items-start justify-between '>
                        <label htmlFor="email">Email:</label>
                        <input className='text-black' style={{width:"300px"}}
                            required
                            id="email"
                            name="email"
                            type="text"
                            onChange={formik.handleChange}
                            value={formik.values.email}
                        />
                    </div>

                    <button type="submit" className='border border-white p-1'>Submit</button>

              
                </form>
            </div>
        </>
    )
}

export default ForgetPassword
