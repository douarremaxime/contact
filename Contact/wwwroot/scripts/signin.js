const form = document.querySelector("form");
const errorsWrapper = document.querySelector("#errors-wrapper");

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/Identity/signin", {
      method: "POST",
      body: new URLSearchParams(new FormData(form)),
    });
    switch (response.status) {
      case 204:
        window.location = "/";
        break;
      case 401:
        errorsWrapper.removeAttribute("hidden");
        break;
      default:
        throw new Error(`Unexpected status code: ${response.status}`);
    }
  } catch (e) {
    console.error(e);
  }
});
