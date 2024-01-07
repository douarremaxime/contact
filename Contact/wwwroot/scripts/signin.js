const form = document.querySelector("form");

const errorsWrapper = document.querySelector("#errors");
const errorsList = errorsWrapper.querySelector("ul");

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/Identity/signin", {
      method: "POST",
      body: new URLSearchParams(new FormData(form)),
      headers: {
        "X-Requested-With": "XMLHttpRequest",
      },
    });
    if (response.ok) {
      window.location = "/";
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
