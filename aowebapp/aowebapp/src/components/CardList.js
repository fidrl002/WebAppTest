import React, { useState, useEffect } from 'react';
import CardV3 from "./CardV3";
//import cardData from "../assets/itemData.json"

function CardListSearch() {
    //let cardData = [
    //    { itemId: 1, itemName: "record 1", itemDescription: "record 1 description", itemCost: 15.00, itemImage: "https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" },
    //    { itemId: 2, itemName: "record 2", itemDescription: "record 2 description", itemCost: 10.00, itemImage: "https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" },
    //    { itemId: 3, itemName: "record 3", itemDescription: "record 3 description", itemCost: 5.00, itemImage: "https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg" },
    //]

    const [cardData, setState] = useState([]);
    const [query, setQuery] = useState('');

    useEffect(() => {
        console.log("useEffect");
        fetch(`http://localhost:5228/api/ItemsWebAPI?searchText=${query}`)
            .then(response => response.json())
            .then(data => setState(data))
            .catch(err => {
                console.log(err);
            });
    }, [query])

    function searchQuery() {
        const value = document.querySelector('[name="searchText"]').value;
        setQuery(value);
    }

    console.log("cardData: " + cardData);

    return (
        <div id="cardListSearch">
            <div className="row justify-content-start mb-3">
                <div className="col-3">
                    <input type="text" name="searchText" className="form-control" placeholder="Type your query" />
                </div>
                <div className="col text-left">
                    <button type="button" className="btn btn-primary" onClick={searchQuery}>Search</button>
                </div>
            </div>
            <div id="cardList" className="row justify-content-center">
                {cardData.map((obj) => (
                    <CardV3
                        key={obj.itemId}
                        itemId={obj.itemId}
                        itemName={obj.itemName}
                        itemDescription={obj.itemDescription}
                        itemCost={obj.itemCost}
                        itemImage={obj.itemImage}
                    />
                )
                )
                }
            </div>
        </div>
    )
}

export default CardListSearch;