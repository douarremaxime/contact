const form = document.querySelector("main form");
const errorWrapper = form.nextElementSibling;
const password = form.querySelector("#password");
const confirmPassword = form.querySelector("#confirmPassword");

confirmPassword.addEventListener("input", () => {
  if (confirmPassword.value !== password.value) {
    confirmPassword.setCustomValidity("Passwords do not match.");
  } else {
    confirmPassword.setCustomValidity("");
  }
});

form.addEventListener("submit", async (event) => {
  event.preventDefault();
  try {
    const response = await fetch("/api/Identity/signup", {
      method: "POST",
      body: new URLSearchParams(new FormData(form)),
      headers: {
        "X-Requested-With": "XMLHttpRequest",
      },
    });
    if (response.ok) {
      window.location = "/signup-successful.html";
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
        errorWrapper.appendChild(errorList);
      }
      errorWrapper.removeAttribute("hidden");
    }
  } catch (e) {
    console.error(e);
  }
});
