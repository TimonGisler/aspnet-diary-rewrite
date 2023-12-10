<script lang="ts">
    import { AddEntryService } from "$lib/generated";
    import { onMount } from "svelte";

    let isEditMode = true; //mode in which the entry is edited/saved
    let entryOverview: HTMLElement;
    let entryContent: HTMLElement;

    let entryTitle = "";
    let entryText = "";

    // onmount fetch entryOverview
    onMount(() => {
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        entryOverview = document.getElementById("entryOverview")!;
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        entryContent = document.getElementById("entryContent")!;
    });

    async function showOverview() {
        //hide the content and show overview
        entryOverview.classList.remove("hidden");
        entryContent.classList.add("hidden");
    }

    async function showContent() {
        //hide the overview and show content
        entryContent.classList.remove("hidden");
        entryOverview.classList.add("hidden");
    }

    async function saveEntry() {
        AddEntryService.postApiEntry({ title: entryTitle, text: entryText });
    }
</script>

<div class="w-full flex-1 grid grid-cols-1 md:grid-cols-5 auto-cols-max">
    <!-- Entry overview -->
    <!-- both hidden and flex are set because on button click hidden gets removed, and it should then be flex, not block -->
    <div id="entryOverview" class="col-span-1 flex hidden md:flex flex-col p-4">
        <div id="entries">
            <div id="singleEntryOverview">Test1</div>
            <div id="singleEntryOverview">Test2</div>
            <div id="singleEntryOverview">Test3</div>
            <div id="singleEntryOverview">Test4</div>
            <div id="singleEntryOverview">Test5</div>
            <div id="singleEntryOverview">Test6</div>
        </div>

        <button class="btn btn-info w-full mt-auto md:hidden" on:click={showContent}>
            Show Content
        </button>
    </div>

    <!-- The part where the entry gets displayed (and buttons to save/edit etc) -->
    <div
        id="entryContent"
        class="flex md:flex flex-col items-center h-full gap-3 p-4  col-span-4"
    >
        <input
            bind:value={entryTitle}
            type="text"
            placeholder="Title"
            class="input input-bordered w-full"
            disabled={!isEditMode}
        />
        <textarea
            bind:value={entryText}
            class="textarea textarea-bordered resize-none w-full h-4/5"
            placeholder="Text"
            disabled={!isEditMode}
        />

        {#if isEditMode}
            <button class="btn btn-success w-full" on:click={saveEntry}
                >Save</button
            >
        {:else}
            <button
                class="btn btn-success w-full"
                on:click={() => (isEditMode = true)}>Edit</button
            >
        {/if}

        <button class="btn btn-info w-full md:hidden" on:click={showOverview}>
            Show overview
        </button>
    </div>
</div>
