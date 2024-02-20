<template>
    <div class="container">
        <div class="col-8">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">{{ resx.Welcome }}</h5>
                    <p class="card-text">{{ message }}</p>
                    <span v-if="tokenExpired">
                        <ul class="dnnActions dnnClear">
                            <li><a class="dnnPrimaryAction" @click="geturlForInitiateOAuth()">{{ resx.LogHubspot }}</a></li>
                        </ul>
                    </span>
                    <span v-if="!tokenExpired">
                        <div v-if="isLoading" class="spinner"></div>
                        <h6>{{ resx.ThereAreTotalOf }} {{ items.Total }} {{ resx.PostsOnHubSpot }}</h6>
                        <div class="dnnFormMessage dnnFormSuccess col-6" v-if="postMigrated">
                            {{ resx.ATotalOf }} {{ postMigrated }} {{ resx.WereMigratedSuccessfully }}
                        </div>
                        <ul class="dnnActions dnnClear">
                            <li><a class="dnnPrimaryAction" @click="migratePosts()">{{ resx.MigratePosts }}</a></li>
                        </ul>
                    </span>
                </div>
            </div>
        </div>
        <div class="col-4 mx-2">
            <HubSpotSettings />
        </div>
        <div class="col-6">
            <MigrateImage />
        </div>
    </div>
</template>

<script setup>
import { inject, ref, } from 'vue';
import { makeRequest } from '../assets/api.js';
import { getCookie, getUrlBase } from '../assets/utils.js';
import HubSpotSettings from './HubSpotSettings.vue';
import MigrateImage from './MigrateImage.vue';

// Injected dependencies
const resx = inject("resx");
const dnnConfig = inject("dnnConfig");

// Reactive references
const items = ref([]);
const message = ref('');
const tokenExpired = ref(false);
const postMigrated = ref(0);
let isLoading = ref(false);

// Variables
const accessToken = getCookie('access_token');

// Functions
const checkCode = async () => {
    const url = new URL(window.location.href);
    const params = new URLSearchParams(url.search);
    let code = params.get('code');
    let newPathname = '';
    if (!code) {
        const pathSegments = url.pathname.split('/');
        const codeIndex = pathSegments.indexOf('code');
        if (codeIndex !== -1 && pathSegments.length > codeIndex + 1) {
            code = pathSegments[codeIndex + 1];
            newPathname = pathSegments.slice(0, codeIndex).join('/');
        }
    }
    if (code) {
        var endpoint = `${getUrlBase()}Hubspot/OAuthCallback`
        const result = await makeRequest(dnnConfig, endpoint, 'post', code, accessToken);
        if (result.access_token != null) {
            document.cookie = `access_token=${result.access_token}; path=/`;
        }
        const pathSegments = url.pathname.split('/');
        const codeIndex = pathSegments.indexOf('code');
        if (codeIndex !== -1) {
            newPathname = pathSegments.slice(0, codeIndex).join('/');
        }
        var redirectUrl = `${url.origin}${newPathname}`;
        window.location.href = redirectUrl;
    }
}

const getBlogPosts = async () => {
    var endpoint = `${getUrlBase()}Hubspot/GetBlogPosts`
    const result = await makeRequest(dnnConfig, endpoint, 'get', null, accessToken);
    if (result) {
        if (result.Status == "error") {
            tokenExpired.value = true;
            message.value = result.Message;
        }
        items.value = result;
    }
}
async function geturlForInitiateOAuth() {
    var endpoint = `${getUrlBase()}Hubspot/InitiateOAuth`
    var url = await makeRequest(dnnConfig, endpoint);
    window.open(url, '_blank');
}

async function migratePosts() {
    isLoading.value = true;
    var endpoint = `${getUrlBase()}Hubspot/MigratePosts`
    const result = await makeRequest(dnnConfig, endpoint, 'get', null, accessToken);
    isLoading.value = false;
    if (result) {
        postMigrated.value = result;
    }
}

// Executed methods during the component's mounting phase
checkCode(); // Check for code in the URL
getBlogPosts();// Initial fetching of items
</script>