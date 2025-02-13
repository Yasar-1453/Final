import React from 'react'
import { FaInstagram } from "react-icons/fa6";
import { FaWhatsapp } from "react-icons/fa";
import { FaTiktok } from "react-icons/fa";
import { FaTwitter } from "react-icons/fa";
function Footer() {
  return (
    <>
      <div className='cont flex justify-between pt-9'>
        <div>
          <p>Contacts:</p>
          <div>
            <p>games_store@gmail.com</p>
            <p>+994 71 777 77 77</p>
          </div>
        </div>
        <div>
          <p>Resources:</p>
          <div>
            <p>Support</p>
          </div>
        </div>
        <div>
          <p>Social Media:</p>
          <div className='flex gap-2'>
           <a href="https://www.instagram.com/dr.lightt?igsh=MXNrczZjN2ppZmV5bQ=="> <FaInstagram style={{ fontSize: "20px" }} /></a>
            <FaWhatsapp style={{ fontSize: "20px" }} />
            <FaTiktok style={{ fontSize: "20px" }} />
            <FaTwitter style={{ fontSize: "20px" }} />
          </div>
        </div>
      </div>
      <div>
        <p className='cont py-4' style={{fontSize:"12px"}}>© 2025, Games Store, Inc. All rights reserved. Our websites may contain links to other sites and resources provided by third parties. These links are provided for your convenience only. </p>
      </div>
    </>
  )
}

export default Footer
