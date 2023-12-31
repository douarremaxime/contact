const signupForm = document.querySelector("#signup-form");
const signupErrors = document.querySelector("#signup-errors");

signupForm.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/Identity/signup", {
      method: "POST",
      body: new URLSearchParams(new FormData(signupForm)),
    });
    switch (response.status) {
      case 204:
        window.location = "signup-successful.html";
        break;
      case 400:
        const result = await response.json();
        const errors = Object.values(result.errors).map((error) => {
          const li = document.createElement("li");
          li.textContent = error[0];
          return li;
        });
        signupErrors.replaceChildren(...errors);
        break;
      default:
        throw new Error(`Unexpected status code: ${response.status}`);
    }
  } catch (e) {
    console.error(e);
  }
});
