const form = document.querySelector("form");
const errorsWrapper = document.querySelector("#errors-wrapper");

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
      errorsWrapper.removeAttribute("hidden");
    }
  } catch (e) {
    console.error(e);
  }
});
