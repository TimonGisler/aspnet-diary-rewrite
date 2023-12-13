<script lang="ts">
    import { goto } from "$app/navigation";
  import { JWT_TOKEN_LOCAL_STORAGE_KEY } from "$lib/Constants";
  import FadingErrorAlert from "$lib/alerts/FadingErrorAlert.svelte";
  import {
    LoginUserHandlerService,
    type LoginUserCommand,
  } from "$lib/generated";

  let email = "";
  let password = "";

  let showError = false;
  let errorReason = "login was not successfull";

  LoginUserHandlerService.postApiLogin;

  async function handleLogin() {
    let loginCommand: LoginUserCommand = { email: email, password: password };
    let response = LoginUserHandlerService.postApiLogin(loginCommand);

    //extract the jwt token from the response and store it in local storage
    //if the request was not successfull display error message
    response
      .then((jwt) => {
        localStorage.setItem(JWT_TOKEN_LOCAL_STORAGE_KEY, jwt);
        goto("/entry");
      })
      .catch((error) => {
        console.log(error);
        errorReason = error.body;
        showError = true;
      });
  }
</script>

<FadingErrorAlert message={errorReason},  bind:show={showError} />

<div
  class="card w-96 bg-base-100 shadow-xl m-auto card-bordered border-blue-600"
>
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
    <a href="/register" class="link mt-2">Register</a>
  </div>


</div>
