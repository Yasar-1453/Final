import React from 'react'

function Dashboard() {
    return (
        <div className='flex bg-white admin-wrapper'>
            <div className='admin-left' style={{ width: "20%", margin: "0 auto 0 0" }}>

            </div>
            <div className='admin-right text-black text-2xl flex items-center justify-center' style={{ width: "80%", margin: "0 0 0 auto", height: "100vh" }}>
                <p> Welcome to Dashboard!</p>
            </div>
        </div>
    )
}

export default Dashboard
