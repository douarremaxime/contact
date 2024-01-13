const form = document.querySelector("main form");
const errorWrapper = form.nextElementSibling;

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/api/Identity/signout-all", {
      method: "POST",
      headers: {
        "X-Requested-With": "XMLHttpRequest",
      },
    });
    if (response.ok) {
      windows.location = "/signin.html";
    } else {
      const result = await response.json();
      const title = document.createElement("p");
      title.textContent = result.title;
      errorWrapper.replaceChildren(title);
      if (Object.hasOwn(result, "errors")) {
        const errorList = document.createElement("ul");
        const errors = Object.values(result.errors).map((error) => {
          const li = document.createElement("li");
          li.textContent = error[0];
          return li;
        });
        errorList.append(...errors);
        errors.appendChild(errorList);
      }
      errorWrapper.removeAttribute("hidden");
    }
  } catch (e) {
    console.error(e);
  }
});
