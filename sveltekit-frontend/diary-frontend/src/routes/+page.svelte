<script>
    import "../app.postcss";
    import Navbar from "$lib/Navbar.svelte";
    import { goto } from "$app/navigation";
</script>
  

<Navbar />

<div class="text-center pt-10">
    <h1>Welcome to this simple Diary site.</h1>
    <button class="btn btn-info my-5" on:click={() => goto("/entry")}>Create first entry</button>
</div>

