<script>
  import Navbar from "$lib/Navbar.svelte";
  import { onMount } from "svelte";
  import { goto } from "$app/navigation";
  import { JWT_TOKEN_LOCAL_STORAGE_KEY } from "$lib/Constants";
  import { OpenAPI } from "$lib/generated";

  onMount(() => {
    // If the user is not logged in (no jwt), redirect to the login page else set the token in the OpenAPI client
    const token = localStorage.getItem(JWT_TOKEN_LOCAL_STORAGE_KEY);
    if (!token) {
      goto("/login");
    } else {
      OpenAPI.TOKEN = token;
    }
  });
</script>

<Navbar />

<!-- only render after the token was succesfully found and set -->
{#if OpenAPI.TOKEN}
  <slot />
{/if}

