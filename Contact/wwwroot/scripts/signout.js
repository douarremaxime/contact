const form = document.querySelector("header form");

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
    }
  } catch (e) {
    console.error(e);
  }
});
