const form = document.querySelector("header form");
const errorWrapper = form.nextElementSibling;

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/api/Identity/signout", {
      method: "POST",
      headers: {
        "X-Requested-With": "XMLHttpRequest",
      },
    });
    if (response.ok) {
      window.location = "/signin.html";
    } else {
      const result = await response.json();
      errorWrapper.textContent = result.title;
      errorWrapper.removeAttribute("hidden");
    }
  } catch (e) {
    console.error(e);
  }
});
