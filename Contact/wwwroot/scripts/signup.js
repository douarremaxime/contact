const form = document.querySelector("main form");
const problemDetails = form.nextElementSibling;
const password = form.querySelector("#password");
const confirmPassword = form.querySelector("#confirmPassword");

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
    const response = await fetch("/api/Identity/signup", {
      method: "POST",
      body: new URLSearchParams(new FormData(form)),
      headers: {
        "X-Requested-With": "XMLHttpRequest",
      },
    });
    if (response.ok) {
      window.location = "/signup-successful.html";
    } else {
      const result = await response.json();
      problemDetails.setProblemDetails(result);
    }
  } catch (e) {
    console.error(e);
  }
});
