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
                        <p class="card-text">{{ resx.ThereAreTotalOf }} {{ items.Total }} {{ resx.PostsOnHubSpot }}</p>
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
    </div>
</template>

<script setup>
import { inject, ref, } from 'vue';
import { makeRequest } from '../assets/api.js';
import { getCookie } from '../assets/utils.js';
import HubSpotSettings from './HubSpotSettings.vue';

// Injected dependencies
const resx = inject("resx");
const dnnConfig = inject("dnnConfig");

// Reactive references
const items = ref([]);
const message = ref('');
const tokenExpired = ref(false);
const postMigrated = ref(0);

// Variables
const accessToken = getCookie('access_token');

// Functions
const checkCode = async () => {
    const url = new URL(window.location.href);
    const params = new URLSearchParams(url.search);
    const code = params.get('code');
    if (code) {
        const result = await makeRequest(dnnConfig, 'OAuthCallback', 'post', code, accessToken);
        if (result.access_token != null) {
            document.cookie = `access_token=${result.access_token}; path=/`;
        }
        const url = new URL(window.location.href);
        window.location.href = url.origin + url.pathname;
    }
}

const getBlogPosts = async () => {
    const result = await makeRequest(dnnConfig, 'GetBlogPosts', 'get', null, accessToken);
    if (result) {
        if (result.Status == "error") {
            tokenExpired.value = true;
            message.value = result.Message;
        }
        items.value = result;
    }
}
async function geturlForInitiateOAuth() {
    var url = await makeRequest(dnnConfig, 'InitiateOAuth');
    window.open(url, '_blank');
}

async function migratePosts() {
    const result = await makeRequest(dnnConfig, 'MigratePosts', 'get', null, accessToken);
    if (result) {
        console.log(result);
        postMigrated.value = result;
    }
}

// Executed methods during the component's mounting phase
checkCode(); // Check for code in the URL
getBlogPosts();// Initial fetching of items
</script>