// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Initialize tooltips
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})

// Initialize popovers
var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl)
})

// Add to favorites
function addToFavorites(id, type) {
    fetch(`/Favorite/Add${type}/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
    .then(response => {
        if (response.ok) {
            const button = document.querySelector(`#favorite-${type.toLowerCase()}-${id}`);
            button.classList.add('active');
            button.setAttribute('onclick', `removeFromFavorites(${id}, '${type}')`);
        }
    })
    .catch(error => console.error('Error:', error));
}

// Remove from favorites
function removeFromFavorites(id, type) {
    fetch(`/Favorite/Remove/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
    .then(response => {
        if (response.ok) {
            const button = document.querySelector(`#favorite-${type.toLowerCase()}-${id}`);
            button.classList.remove('active');
            button.setAttribute('onclick', `addToFavorites(${id}, '${type}')`);
        }
    })
    .catch(error => console.error('Error:', error));
}

// Add comment
function addComment(id, type) {
    const content = document.querySelector(`#comment-${type.toLowerCase()}-${id}`).value;
    if (!content) return;

    fetch(`/Comment/Add${type}Comment/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify({ content: content })
    })
    .then(response => {
        if (response.ok) {
            location.reload();
        }
    })
    .catch(error => console.error('Error:', error));
}

// Delete comment
function deleteComment(id) {
    if (!confirm('Are you sure you want to delete this comment?')) return;

    fetch(`/Comment/Delete/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
    .then(response => {
        if (response.ok) {
            location.reload();
        }
    })
    .catch(error => console.error('Error:', error));
}