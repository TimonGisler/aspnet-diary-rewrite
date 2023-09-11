<script lang="ts">
  import { RegisterUserHandlerService, type RegisterUserCommand } from "$lib/generated";

  let email = "";
  let password = "";
  let repeatedPassword = ""; //TODO TGIS, add method which checks if passwords are the same if either password changes

  async function handleRegister() {
    let registerCommand: RegisterUserCommand = {
      email: email,
      password: password,
    };

    RegisterUserHandlerService.postApiRegister(registerCommand); 
  }

</script>

<div class="card w-96 bg-base-100 shadow-xl m-auto card-bordered border-blue-600">
  <div class="card-body">
    <h1 class="text-center text-3xl mb-3">Register</h1>

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
    <input
      bind:value={repeatedPassword}
      type="text"
      placeholder="Repeat Password"
      class="input input-bordered input-info w-full max-w-xs"
    />

    <button class="btn btn-success mt-7" on:click={handleRegister}>Register</button>
  </div>
</div>
