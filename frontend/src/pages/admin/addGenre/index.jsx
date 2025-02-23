import React, { useEffect, useState } from 'react'
import { useFormik } from 'formik';
import axios from "axios"
import { useNavigate } from 'react-router-dom'
let DBurl = "http://localhost:5156/api/Genre"


function AddGenre() {
    let navigate = useNavigate()

  const formik = useFormik({
    initialValues: {
      name: ''

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

          console.log('AddGenre successful', response.data);
        } else {
          alert("Invalid genre!");
        }
      } catch (error) {
        console.error('addGenre failed:', error.response ? error.response.data.errors : error);
        alert("An error occurred while adding. Please try again.");
      }
    },
  })
  return (
    <div className='flex bg-white'>
    <div className='admin-left' style={{ backgroundColor: "white", width: "20%", margin: "0 auto 0 0" }}>

    </div>
    <div className='admin-right text-black' style={{ backgroundColor: "white", paddingTop: "200px", width: "80%", margin: "0 0 0 auto", height: "100vh" }}>
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

            <button type="submit" className='mt-2 admin-btn'>Submit</button>
        </form>
    </div>
</div>
  )
}

export default AddGenre
