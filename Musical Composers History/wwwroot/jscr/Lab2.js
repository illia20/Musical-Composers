const uri = 'api/Instruments';
let instruments = [];

function getInstruments() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayInstruments(data))
        .catch(error => console.error('Unable to get instruments.', error));
}

function addInstrument() {
    const addNameTextbox = document.getElementById('add-name');
    const addInfoTextbox = document.getElementById('add-info');
    const Instrument = {
        name: addNameTextbox.value.trim(),
        info: addInfoTextbox.value.trim(),
    };
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Instrument)
    })
        .then(response => response.json())
        .then(() => {
            getInstruments();
            addNameTextbox.value = '';
            addInfoTextbox.value = '';
        })
        .catch(error => console.error('Unable to add instrument.', error));
}

function deleteInstrument(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getInstruments())
        .catch(error => console.error('Unable to delete instrument.', error));
}

function displayEditForm(id) {
    const Instrument = instruments.find(Instrument => Instrument.id === id);

    document.getElementById('edit-id').value = Instrument.id;
    document.getElementById('edit-name').value = Instrument.name;
    document.getElementById('edit-info').value = Instrument.info;
    document.getElementById('editForm').style.display = 'block';
}

function updateInstrument() {
    const instrumentId = document.getElementById('edit-id').value;
    const Instrument = {
        id: parseInt(instrumentId, 10),
        name: document.getElementById('edit-name').value.trim(),
        info: document.getElementById('edit-info').value.trim()
    };
    fetch(`${uri}/${instrumentId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Instrument)
    }
    )
        .then(() => getInstruments())
        .catch(error => console.error('Unable to update instrument', error));
    closeInput();
    getInstruments();
    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayInstruments(data) {
    const tBody = document.getElementById('instruments');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(Instrument => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${Instrument.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteInstrument(${Instrument.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(Instrument.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(Instrument.info);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });
    instruments = data;
}