<script lang="ts">
  import { goto } from "$app/navigation";
  import { onMount } from "svelte";
  import { JWT_TOKEN_LOCAL_STORAGE_KEY } from "./Constants";

  // If the user is not logged in (no jwt), redirect to the login page else set the token in the OpenAPI client
  let isLoggedIn = false;

  onMount(() => {
    isLoggedIn = localStorage.getItem(JWT_TOKEN_LOCAL_STORAGE_KEY) !== null;
  });

  async function logout() {
    localStorage.removeItem(JWT_TOKEN_LOCAL_STORAGE_KEY);
    goto("/login");
  }
</script>

<div class="navbar bg-base-100 flex justify-between px-10">
  <a href="/" class=" normal-case text-xl">diary</a>

  {#if isLoggedIn}
    <button on:click={logout} class="btn btn-warning btn-sm">logout</button>
  {:else}
    <button on:click={() => goto("/login")} class="btn btn-success btn-sm"
      >login</button
    >
  {/if}
</div>
