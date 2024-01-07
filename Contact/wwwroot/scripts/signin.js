const form = document.querySelector("form");
const errors = document.querySelector("#errors");

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/Identity/signin", {
      method: "POST",
      body: new URLSearchParams(new FormData(form)),
    });
    if (response.ok) {
      window.location = "/";
    } else {
      errors.removeAttribute("hidden");
    }
  } catch (e) {
    console.error(e);
  }
});
