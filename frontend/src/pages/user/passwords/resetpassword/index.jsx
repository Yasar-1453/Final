import React from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Auth/ResetPassword"
import { NavLink } from 'react-router-dom';

function ResetPassword() {
  let navigate = useNavigate()

  const formik = useFormik({
    initialValues: {
      newPassword: '',
      confirmNewPassword:''

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
        <p className='text-center'>Write new password to reset old one</p>
        <form onSubmit={formik.handleSubmit} className='flex flex-col items-center gap-2 my-7'>
          <div className='flex flex-col items-start justify-between '>
            <label htmlFor="newPassword">New Password:</label>
            <input className='text-black' style={{ width: "300px" }}
              required
              id="newPassword"
              name="newPassword"
              type="text"
              onChange={formik.handleChange}
              value={formik.values.newPassword}
            />
          </div>
          <div className='flex flex-col items-start justify-between '>
            <label htmlFor="confirmNewPassword">Confirm New Password:</label>
            <input className='text-black' style={{ width: "300px" }}
              required
              id="confirmNewPassword"
              name="confirmNewPassword"
              type="text"
              onChange={formik.handleChange}
              value={formik.values.confirmNewPassword}
            />
          </div>

          <button type="submit" className='border border-white p-1'>Submit</button>


        </form>
      </div>
    </>
  )
}

export default ResetPassword
