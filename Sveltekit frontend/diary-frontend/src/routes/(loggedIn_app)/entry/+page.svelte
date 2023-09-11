<script lang="ts">
    import { SERVER_URL } from "$lib/Constants";

    let entryTitle =  "";
    let entryText = "";

    async function saveEntry() {
        let response = await fetch(SERVER_URL + "/entries", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                title: entryTitle,
                text: entryText,
            }),
        });

        if (response.status == 200) {
            console.log("Entry saved successfully");
        } else {
            console.log("Entry save failed" + response.status + " " + response.statusText);
        }
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
        placeholder="Bio"
    />

    <button class="btn btn-success w-full" on:click={saveEntry}>Save</button>
</div>
