<template>
    <div class="card">
        <div class="card-body custom-module">
            <div class="dnnFormMessage dnnFormInfo">{{ resx.YouHaveMigrateAfter }}</div>
            <div class="mb-2">
                <label class="text-bold">{{ resx.SelectOptionToAdjustImages }}</label>
                <div>
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="radio" v-model="typeOfMigration" id="migrate"
                            value="migrate" :checked="typeOfMigration === 'migrate'">
                        <label class="form-check-label" for="migrate"> {{ resx.TransferTheMainImage }}</label>
                    </div>
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="radio" v-model="typeOfMigration" id="update"
                            value="update" :checked="typeOfMigration === 'update'">
                        <label class="form-check-label" for="update"> {{ resx.UpdateImageReferences }}</label>
                    </div>
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="radio" v-model="typeOfMigration" id="download"
                            value="download" :checked="typeOfMigration==='download'">
                        <label class=" form-check-label" for="download"> {{ resx.RetrieveLocallyStoreImages
                            }}</label>
                    </div>
                </div>
            </div>
            <h5><span v-if="numberMigratedImages > 0">
                    {{ resx.ThereAreTotalOf }} {{ numberMigratedImages }} {{ resx.ImagesWereMigratedSuccessfully }}
                </span>
                {{ numberMigratedImages == 0 && numberMigratedImages != null ? resx.NoImagesMigrated : '' }} </h5>
            <div v-if="isLoading" class="spinner"></div>
            <form @submit="handleSubmit" class="dnnForm">
                <div class="dnnFormItem">
                    <label for="originFolderPath" class="dnnLabel">{{ typeOfMigration === 'download' ? resx.PathToSaveImages :
                        resx.OriginFolderPath }}</label>
                    <input v-model="originFolderPath" type="text" id="originFolderPath" class="dnnFormInput" required />
                </div>
                <div class="dnnActions dnnClear">
                    <button type="submit" class="dnnPrimaryAction">{{ typeOfMigration === 'update' ? resx.Update
                        : typeOfMigration ===
                        'download' ? resx.Download :
                        resx.MigrateImage
                        }}</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script setup>
import { inject, ref, } from 'vue';
import { makeRequest } from '../assets/api.js';
import { getUrlBase } from "../assets/utils.js";

// Injected dependencies
const dnnConfig = inject("dnnConfig");
const resx = inject("resx");

let originFolderPath = ref('');
let numberMigratedImages = ref(null);
let isLoading = ref(false);
let typeOfMigration = ref('migrate');

const handleSubmit = async (event) => {
    event.preventDefault();
    isLoading.value = true;
    var endpoint = `${getUrlBase()}EasyDNNNews/MigrateImagesToEasyDNNNews`
    if (typeOfMigration.value === 'download') {
        endpoint = `${getUrlBase()}Hubspot/DonwLoadImageInPostContent`
    }
    if (typeOfMigration.value === 'update') {
        endpoint = `${getUrlBase()}Hubspot/UpdateUrlInPostContent`
    }       
    const response = await makeRequest(dnnConfig, endpoint, 'post', originFolderPath.value);
    isLoading.value = false;
    numberMigratedImages.value = response;
};
</script>
