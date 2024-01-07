const form = document.querySelector("form");

const password = document.querySelector("#password");
const confirmPassword = document.querySelector("#confirm-password");

const errorsWrapper = document.querySelector("#errors");
const errorsList = errorsWrapper.querySelector("ul");

confirmPassword.addEventListener("input", () => {
  if (confirmPassword.value !== password.value) {
    confirmPassword.setCustomValidity("Passwords do not match.");
  } else {
    confirmPassword.setCustomValidity("");
  }
});

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/Identity/signup", {
      method: "POST",
      body: new URLSearchParams(new FormData(form)),
    });
    if (response.ok) {
      window.location = "/signup-successful.html";
    } else {
      const result = await response.json();
      const errors = Object.values(result.errors).map((error) => {
        const li = document.createElement("li");
        li.textContent = error[0];
        return li;
      });
      errorsList.replaceChildren(...errors);
      errorsWrapper.removeAttribute("hidden");
    }
  } catch (e) {
    console.error(e);
  }
});
