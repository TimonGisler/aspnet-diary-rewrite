<script>
  import Navbar from "$lib/Navbar.svelte";
  import { onMount } from 'svelte';
  import { goto } from '$app/navigation';
  import { JWT_TOKEN_LOCAL_STORAGE_KEY } from "$lib/Constants";

  onMount(() => {
    const userToken = localStorage.getItem(JWT_TOKEN_LOCAL_STORAGE_KEY);

    if (!userToken) {
      goto('/login');
    }
  });

</script>

<Navbar />

<slot />
