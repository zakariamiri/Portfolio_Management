const radios = document.querySelectorAll("input[name='role']");
const btn = document.querySelector(".create-account-btn");

let selectedRole = null;

radios.forEach(radio => {
    radio.addEventListener("change", function () {
        selectedRole = this.value;
        if (selectedRole === "recruiter") {
            btn.textContent = "Join as a Recruiter";
        } else if (selectedRole === "creator") {
            btn.textContent = "Join as a Portfolio Creator";
        }
    });
});

btn.addEventListener("click", function () {
    if (!selectedRole) {
        alert("Please select an option first.");
        return;
    }
    if (selectedRole === "recruiter") {
        window.location.href = btn.dataset.recruiterUrl;
    } else if (selectedRole === "creator") {
        window.location.href = btn.dataset.creatorUrl;
    }
});
