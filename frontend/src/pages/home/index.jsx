import React, { useEffect, useState } from 'react'
const images = [
"https://4kwallpapers.com/images/walls/thumbs_3t/18719.jpg",
"https://media.istockphoto.com/id/498301640/ru/%D0%B2%D0%B5%D0%BA%D1%82%D0%BE%D1%80%D0%BD%D0%B0%D1%8F/big-%D0%BF%D1%80%D0%BE%D0%B4%D0%B0%D0%B6%D0%B0-%D0%B1%D0%B0%D0%BD%D0%BD%D0%B5%D1%80.jpg?s=612x612&w=0&k=20&c=-TS75F7hE9QS9mO8B9Sl6ANDhpg1v3DHFE2r1_SRT8A=",
"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQIfnLSNVrotMH_cY2Loik03WkpG6n9A5x5Dw&s"
];
import { TfiArrowCircleLeft } from "react-icons/tfi";
import { TfiArrowCircleRight } from "react-icons/tfi";


function Home() {
  const [currentIndex, setCurrentIndex] = useState(0);

  const nextSlide = () => {
    setCurrentIndex((prevIndex) => (prevIndex + 1) % images.length);
  };

  const prevSlide = () => {
    setCurrentIndex((prevIndex) =>
      prevIndex === 0 ? images.length - 1 : prevIndex - 1
    );
  };

  useEffect(() => {
    const interval = setInterval(() => {
      nextSlide();
    }, 4000); 
    return () => clearInterval(interval);
  }, []);
  return (
    <div>
 <div className="slider-container">
      <div className="slider">
        <img src={images[currentIndex]} alt={`Slide ${currentIndex + 1}`} />
      </div>
      <button className="prev" onClick={prevSlide}>
      <TfiArrowCircleLeft />
      </button>
      <button className="next" onClick={nextSlide}>
      <TfiArrowCircleRight />
      </button>
    </div>
    </div>
  )
}

export default Home
