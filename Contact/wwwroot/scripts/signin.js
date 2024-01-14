const form = document.querySelector("main form");
const problemDetails = form.nextElementSibling;

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("api/Identity/signin", {
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
      problemDetails.setProblemDetails(result);
    }
  } catch (e) {
    console.error(e);
  }
});
