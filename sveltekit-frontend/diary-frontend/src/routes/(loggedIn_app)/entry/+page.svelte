<script lang="ts">
    import {
        AddEntryService,
        GetEntryOverviewHandlerService,
        type EntryOverview,
        GetSpecificEntryHandlerService,
        type EntryData,
    } from "$lib/generated";
    import { onMount } from "svelte";

    let entryOverviewData: EntryOverview[] = [];

    let isEditMode = true; //mode in which the entry is edited/saved

    let entryOverviewSidebarId = "entryOverviewSidebar";
    let entryContentId = "entryContent";
    let entryOverviewSidebar: HTMLElement; //needed to hide/show the entry overview in mobile
    let entryContent: HTMLElement; //needed to hide/show the entry overview in mobile

    let currentEntry: EntryData = {
        id: -1,
        title: "",
        text: "",
        created:""
    };

    onMount(() => {
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        entryOverviewSidebar = document.getElementById(entryOverviewSidebarId)!;
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        entryContent = document.getElementById(entryContentId)!;

        fetchOverview();
    });

    async function fetchOverview() {
        //fetch the overview of the entries
        let entryOverviewResponse =
            GetEntryOverviewHandlerService.getApiEntry();
        entryOverviewResponse.then((response) => {
            //sort response by id (so that newest entry is on top)
            response.sort((a, b) => b.entryId - a.entryId);
            entryOverviewData = response;
        });
    }

    async function showOverview() {
        //hide the content and show overview
        entryOverviewSidebar.classList.remove("hidden");
        entryContent.classList.add("hidden");
    }

    async function showContent() {
        //hide the overview and show content
        entryContent.classList.remove("hidden");
        entryOverviewSidebar.classList.add("hidden");
    }
    async function switchToEditMode() {
        isEditMode = true;
    }

    async function switchToReadMode() {
        isEditMode = false;
    }

    async function showEntry(entryId: number) {
        let response = GetSpecificEntryHandlerService.getApiEntry(entryId);
        response.then((response) => {
            currentEntry = response;
        });

        switchToReadMode();
    }

    async function saveEntry() {
        AddEntryService.postApiEntry({
            title: currentEntry.title,
            text: currentEntry.text,
        }).then(() => {
            fetchOverview();
            switchToReadMode();
        });
    }
</script>

<div class="w-full flex-1 grid grid-cols-1 md:grid-cols-5 auto-cols-max">
    <!-- Entry overview -->
    <!-- both hidden and flex are set because on button click hidden gets removed, and it should then be flex, not block -->
    <div
        id={entryOverviewSidebarId}
        class="col-span-1 flex hidden md:flex flex-col p-4"
    >
        <div id="entries" class="flex flex-col gap-3">
            {#each entryOverviewData as entry (entry.entryId)}
                <button
                    id="singleEntryOverview"
                    class=" bg-black p-3"
                    on:click={() => showEntry(entry.entryId)}
                    >{entry.title}</button
                >
            {/each}
        </div>

        <button
            class="btn btn-info w-full mt-auto md:hidden"
            on:click={showContent}
        >
            Show Content
        </button>
    </div>

    <!-- The part where the entry gets displayed (and buttons to save/edit etc) -->
    <div
        id={entryContentId}
        class="flex md:flex flex-col items-center h-full gap-3 p-4 col-span-4"
    >
        <input
            bind:value={currentEntry.title}
            type="text"
            placeholder="Title"
            class="input input-bordered w-full"
            disabled={!isEditMode}
        />
        <textarea
            bind:value={currentEntry.text}
            class="textarea textarea-bordered resize-none w-full h-4/5"
            placeholder="Text"
            disabled={!isEditMode}
        />

        {#if isEditMode}
            <button class="btn btn-success w-full" on:click={saveEntry}
                >Save</button
            >
        {:else}
            <button class="btn btn-success w-full" on:click={switchToEditMode}
                >Edit</button
            >
        {/if}

        <button class="btn btn-info w-full md:hidden" on:click={showOverview}>
            Show overview
        </button>
    </div>
</div>
