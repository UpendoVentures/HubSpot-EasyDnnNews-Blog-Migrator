<template>
    <div class="card">
        <div class="card-body custom-module">
            <div class="dnnFormMessage dnnFormInfo">{{ resx.YouHaveMigrateAfter }}</div>
            <h5><span v-if="numberMigratedImages > 0">
                    {{ resx.ThereAreTotalOf }} {{ numberMigratedImages }} {{ resx.ImagesWereMigratedSuccessfully }}
                </span>
                {{ numberMigratedImages == 0 && numberMigratedImages != null ? resx.NoImagesMigrated : '' }} </h5>
            <div v-if="isLoading" class="spinner"></div>
            <form @submit="handleSubmit" class="dnnForm">
                <div class="dnnFormItem">
                    <label for="originFolderPath" class="dnnLabel">{{ resx.OriginFolderPath }}</label>
                    <input v-model="originFolderPath" type="text" id="originFolderPath" class="dnnFormInput" required />
                </div>
                <div class="dnnActions dnnClear">
                    <button type="submit" class="dnnPrimaryAction">{{ resx.MigrateImage }}</button>
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

const handleSubmit = async (event) => {
    event.preventDefault();
    isLoading.value = true;
    var endpoint = `${getUrlBase()}EasyDNNNews/MigrateImagesToEasyDNNNews`
    const response = await makeRequest(dnnConfig, endpoint, 'post', originFolderPath.value);
    isLoading.value = false;
    numberMigratedImages.value = response;
    console.log(response);
};
</script>
