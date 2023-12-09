<!-- Error that will fade in and out -->
<script lang="ts">
    import { fade } from "svelte/transition";

    export let message: string;
    export let show = true;

</script>

{#if show}
    <div role="alert" class="alert alert-error max-w-2xl self-center flex absolute mt-5" out:fade={{duration: 1000 }} in:fade={{ duration: 200 }}>
        <svg xmlns="http://www.w3.org/2000/svg" class="stroke-current shrink-0 h-6 w-6" fill="none" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
        <span>{message}</span>
    </div>
{/if}