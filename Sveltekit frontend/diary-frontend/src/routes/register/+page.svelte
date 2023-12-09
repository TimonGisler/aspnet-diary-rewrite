<script lang="ts">
  import {
    RegisterUserHandlerService,
    type RegisterUserCommand,
    CancelablePromise,
  } from "$lib/generated";
  import { goto } from "$app/navigation";
  import FadingErrorAlert from "$lib/alerts/FadingErrorAlert.svelte";

  let email = "";
  let password = "";
  let repeatedPassword = ""; //TODO TGIS, add method which checks if passwords are the same if either password changes

  //Flag when toggled error will be shown
  let errorReason = "";
  let showError = false;

  async function handleRegister() {
    let registerCommand: RegisterUserCommand = {
      email: email,
      password: password,
    };

    let registerResponse: CancelablePromise<unknown> =
      RegisterUserHandlerService.postApiRegister(registerCommand);
    //if user registered successfully redirect to login page else display error message
    registerResponse
      .then(() => {
        goto("/login");
      })
      .catch((error) => {
        //display error message
        errorReason = error.body;
        showError = true;
      });
  }
</script>

<!-- If error flag is set display error -->
<FadingErrorAlert message="{errorReason}," bind:show={showError} />

<div
  class="card w-full max-w-sm bg-base-100 shadow-xl m-auto card-bordered border-blue-600"
>
  <div class="card-body">
    <h1 class="text-center text-3xl mb-3">Register</h1>

    <input
      bind:value={email}
      type="text"
      placeholder="Email"
      class="input input-bordered input-info w-full max-w-xs"
    />

    <!-- TODO TGIS, change type to password -->
    <input
      bind:value={password}
      type="text"
      placeholder="Password"
      class="input input-bordered input-info w-full max-w-xs"
    />
    <input
      bind:value={repeatedPassword}
      type="text"
      placeholder="Repeat Password"
      class="input input-bordered input-info w-full max-w-xs"
    />

    <button class="btn btn-success mt-7" on:click={handleRegister}
      >Register</button
    >
    <a href="/login" class="link link-info font-bold">Login</a>
  </div>
</div>
