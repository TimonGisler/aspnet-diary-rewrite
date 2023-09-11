<script lang="ts">
  import { SERVER_URL } from "$lib/Constants";

  let email = "";
  let password = "";

  async function handleLogin() {
    let wasLoginSuccessful = false;

    let response = await fetch(SERVER_URL + "/login", {
      method: "POST",
      headers: { Authorization: "Basic " + btoa(email + ":" + password) },
    });

    wasLoginSuccessful = response.status == 200;
    console.log("login was: " + wasLoginSuccessful);
  }
</script>

<div class="card w-96 bg-base-100 shadow-xl m-auto card-bordered border-blue-600">
  <div class="card-body">
    <h1 class="text-center text-3xl mb-3">Login</h1>

    <input
      bind:value={email}
      type="text"
      placeholder="Email"
      class="input input-bordered input-info w-full max-w-xs"
    />
    <input
      bind:value={password}
      type="text"
      placeholder="Password"
      class="input input-bordered input-info w-full max-w-xs"
    />

    <button class="btn btn-success mt-7" on:click={handleLogin}>Login</button>
  </div>
</div>
