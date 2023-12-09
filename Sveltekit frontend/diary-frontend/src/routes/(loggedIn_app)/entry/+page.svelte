<script lang="ts">
    import { AddEntryService } from "$lib/generated";

    let entryTitle = "";
    let entryText = "";

    async function saveEntry() {
        AddEntryService.postApiEntry({ title: entryTitle, text: entryText });
    }
</script>

<div class="flex flex-col items-center h-full gap-3 mt-4 p-4">
    <input
        bind:value={entryTitle}
        type="text"
        placeholder="Title"
        class="input input-bordered w-full"
    />
    <textarea
        bind:value={entryText}
        class="textarea textarea-bordered resize-none w-full h-4/5"
        placeholder="Text"
    />

    <button class="btn btn-success w-full" on:click={saveEntry}>Save</button>
</div>
