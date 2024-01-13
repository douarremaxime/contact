const form = document.querySelector("main form");
const error = form.nextElementSibling;

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/api/Identity/signin", {
      method: "POST",
      body: new URLSearchParams(new FormData(form)),
      headers: {
        "X-Requested-With": "XMLHttpRequest",
      },
    });
    if (response.ok) {
      window.location = "/";
    } else {
      error.removeAttribute("hidden");
    }
  } catch (e) {
    console.error(e);
  }
});
