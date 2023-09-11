/** @type {import('tailwindcss').Config}*/
const config = {
  content: ["./src/**/*.{html,js,svelte,ts}"],

  theme: {
    extend: {},
  },

  daisyui: {
    themes: ["halloween"],
  },

  plugins: [require("daisyui")],
};

module.exports = config;
