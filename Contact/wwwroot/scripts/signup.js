const form = document.querySelector("#form");
const errorsWrapper = document.querySelector("#errors-wrapper");
const errorsList = document.querySelector("#errors-list");

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/Identity/signup", {
      method: "POST",
      body: new URLSearchParams(new FormData(form)),
    });
    switch (response.status) {
      case 204:
        window.location = "/signup-successful.html";
        break;
      case 400:
        const result = await response.json();
        const errors = Object.values(result.errors).map((error) => {
          const li = document.createElement("li");
          li.textContent = error[0];
          return li;
        });
        errorsList.replaceChildren(...errors);
        errorsWrapper.removeAttribute("hidden");
        break;
      default:
        throw new Error(`Unexpected status code: ${response.status}`);
    }
  } catch (e) {
    console.error(e);
  }
});
