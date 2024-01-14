class ProblemDetailsElement extends HTMLElement {
  constructor() {
    super();
    const shadow = this.attachShadow({ mode: "open" });
    this.errorTitle = document.createElement("p");
    this.errorList = document.createElement("ul");
    shadow.appendChild(this.errorTitle);
    shadow.appendChild(this.errorList);
  }

  setProblemDetails(result) {
    this.errorTitle.textContent = result.title;
    const errors = Object.values(result.errors ?? []).map((error) => {
      const li = document.createElement("li");
      li.textContent = error[0];
      return li;
    });
    this.errorList.replaceChildren(...errors);
  }
}

customElements.define("problem-details", ProblemDetailsElement);
