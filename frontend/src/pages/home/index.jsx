import React, { useState } from 'react'


const images = [
"https://4kwallpapers.com/images/walls/thumbs_3t/18719.jpg",
"https://cdn2.unrealengine.com/egs-civ-7-carousel-mobile-1080x1440-4e5c40f5b22c.png?resize=1&w=640&h=854&quality=medium",
"https://cdn2.unrealengine.com/egs-farming-sim-25-cover-story-carousel-mobile-1200x1600-99ec8f01a63d.jpg?resize=1&w=640&h=854&quality=medium"
];
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

  return (
    <div>
 <div className="slider-container">
      <div className="slider">
        <img src={images[currentIndex]} alt={`Slide ${currentIndex + 1}`} />
      </div>
      <button className="prev" onClick={prevSlide}>
        Prev
      </button>
      <button className="next" onClick={nextSlide}>
        Next
      </button>
    </div>
    </div>
  )
}

export default Home
