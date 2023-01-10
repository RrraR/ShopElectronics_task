import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';

export default function Pagination(props) {
    const {nPages, currentPage, setCurrentPage} = props
    const pageNumbers = [...Array(nPages + 1).keys()].slice(1)

    const nextPage = () => {
        if(currentPage !== nPages)
            setCurrentPage(currentPage + 1)
    }
    const prevPage = () => {
        if(currentPage !== 1)
            setCurrentPage(currentPage - 1)
    }
    
    
    return (
        <div>
            <nav>
                <ul className=' pagination justify-content-center'>
                    <li className='page-item'>
                        <a
                            href="#"
                            onClick={prevPage}
                            className='page-link'>
                            Previous
                        </a>
                    </li>
                    {pageNumbers.map(pgNumber => (
                        <li key={pgNumber}
                            className={`page-item ${currentPage == pgNumber ? 'active' : ''}`}>

                            <a
                                href="#"
                                onClick={() => setCurrentPage(pgNumber)}
                                className='page-link'
                            >
                                {pgNumber}
                            </a>

                        </li>
                    ))}
                    <li className='page-item'>
                        <a
                            href="#"
                            className='page-link'
                            onClick={nextPage}
                        >Next</a>
                    </li>
                </ul>
            </nav>
        </div>
    )
}