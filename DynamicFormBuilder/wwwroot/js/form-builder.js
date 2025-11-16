(function () {

    const container = document.getElementById('fieldsContainer');
    const addBtn = document.getElementById('addMoreBtn');

    // Fixed dropdown options
    const FIXED_OPTIONS = ['Option 1', 'Option 2', 'Option 3'];

    let fieldIndex = 0;

    // Create a new dynamic dropdown field block
    function createField() {

        const wrapper = document.createElement('div');
        wrapper.className = 'field-wrapper';
        wrapper.dataset.index = fieldIndex;

        // Label input
        const labelInput = document.createElement('input');
        labelInput.type = 'text';
        labelInput.name = 'fieldLabel[]';
        labelInput.placeholder = 'Label (e.g., Select Country)';
        labelInput.required = true;
        labelInput.className = 'form-control mb-2';

        // Required checkbox
        const requiredCheckbox = document.createElement('input');
        requiredCheckbox.type = 'checkbox';
        requiredCheckbox.className = 'required-checkbox';

        const requiredWrap = document.createElement('label');
        requiredWrap.appendChild(requiredCheckbox);
        requiredWrap.appendChild(document.createTextNode(' Required'));

        // Hidden required input (to ensure true/false always posted)
        const hiddenRequired = document.createElement('input');
        hiddenRequired.type = 'hidden';
        hiddenRequired.name = 'fieldRequired[]';
        hiddenRequired.value = 'false';  // default

        requiredCheckbox.addEventListener('change', () => {
            hiddenRequired.value = requiredCheckbox.checked ? 'true' : 'false';
        });

        // Dropdown select
        const select = document.createElement('select');
        select.name = 'fieldOption[]';
        select.className = 'form-select mt-2';

        FIXED_OPTIONS.forEach(opt => {
            const option = document.createElement('option');
            option.value = opt;
            option.textContent = opt;
            select.appendChild(option);
        });

        // Remove button
        const removeBtn = document.createElement('button');
        removeBtn.type = 'button';
        removeBtn.textContent = 'Remove';
        removeBtn.className = 'my-2 btn btn-danger';
        removeBtn.addEventListener('click', () => {
            wrapper.remove();
        });

        // Add elements inside wrapper
        wrapper.appendChild(labelInput);
        wrapper.appendChild(requiredWrap);
        wrapper.appendChild(hiddenRequired);
        wrapper.appendChild(select);
        wrapper.appendChild(removeBtn);

        fieldIndex++;
        return wrapper;
    }

    // Add new field on button click
    addBtn.addEventListener('click', () => {
        container.appendChild(createField());
    });

    // Add one field initially
    addBtn.click();

})();
