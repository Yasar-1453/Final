
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
      console.log('Submitting values:', values);

      try {
        const response = await axios.post(DBurl, values, {
          headers: {
            'Content-Type': 'application/json',
          },
        });

        if (response.status === 200 && response.data) {

          console.log('Login successful', response.data);
          localStorage.setItem('Username', values.Username);
          navigate("/");
          window.location.reload();
        } else {
          alert("Invalid login credentials!");
        }
      } catch (error) {
        console.error('Login failed:', error.response ? error.response.data.errors : error);
        alert("An error occurred while logging in. Please try again.");
      }
    },
  })


  return (
    <>
      <div className='cont log'>
        <form onSubmit={formik.handleSubmit} className='flex flex-col items-center gap-2 my-7'>

          <div className='flex flex-col items-start justify-between '>
            <label htmlFor="Username">UserName:</label>
            <input className='text-black'  style={{width:"300px"}}
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
            <input className='text-black'  style={{width:"300px"}}
              required
              id="Password"
              name="Password"
              type="password"
              onChange={formik.handleChange}
              value={formik.values.Password}
            />
          </div>

          <NavLink to="/forgetPassword"><p>Forget password?</p></NavLink>

          <button type="submit" className='border border-white p-1'>Submit</button>

          <p>Dont have an account? <NavLink to="/register" className="underline">Register</NavLink></p>

        </form>
      </div>
    </>
  )
}

export default LogIn

