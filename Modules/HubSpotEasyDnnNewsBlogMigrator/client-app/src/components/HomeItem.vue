<template>
    <div class="mx-3">
        <div class="text-center">
            <h2>{{ resx.Welcome }}</h2>
            <ul class="dnnActions dnnClear">
                <li><a class="dnnPrimaryAction" @click="migratePosts()">Migrate Posts</a></li>
            </ul>
            <ul class="dnnActions dnnClear">
                <li><a class="dnnPrimaryAction" @click="updateSettings()">Update Settings</a></li>
            </ul>
            <ul class="dnnActions dnnClear">
                <li><a class="dnnPrimaryAction" @click="geturlForInitiateOAuth()">Initiate OAuth</a></li>
            </ul>
        </div>
    </div>
</template>

<script setup>
import { inject, ref, } from 'vue';
import { makeRequest } from '../assets/api.js';
import { getCookie } from '../assets/utils.js';

// Injected dependencies
const resx = inject("resx");
const dnnConfig = inject("dnnConfig");

// Reactive references
const items = ref([]);

// Variables
const accessToken = getCookie('access_token');

// Functions
const checkCode = async () => {
    const url = new URL(window.location.href);
    const params = new URLSearchParams(url.search);
    const code = params.get('code');
    if (code) {
        const result = await makeRequest(dnnConfig, 'OAuthCallback', 'post', code, accessToken);
        console.log("result", result);
        if (result.access_token != null) {
            document.cookie = `access_token=${result.access_token}; path=/`;
        }
        // else {
        //     document.cookie = "access_token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        // }
        const url = new URL(window.location.href);
        window.location.href = url.origin + url.pathname;
    }
}

const fetchItems = async () => {
    const result = await makeRequest(dnnConfig, 'GetBlogPosts', 'get', null, accessToken);
    if (result) {
        items.value = result;
    }
}

const getSettings = async () => {
    const result = await makeRequest(dnnConfig, 'GetSettings', 'get', null, accessToken);
    if (result) {
        items.value = result;
    }
}

const updateSettings = async () => {
    var data = {
        ClientId: "e7d75ea8-f8d6-416e-9817-7e3a00b00b82",
        ClientSecret: "cd7713a0-14ce-4a9d-87a5-3c156caf3f6f",
        RedirectUri: "https://localhost.dnndev.me/hubspot",
        Scope: "content"
    }
    const response = await makeRequest(dnnConfig, 'UpdateSettings', 'post', data);
    if (response) {
        fetchItems();
    }
}

async function geturlForInitiateOAuth() {
    var url = await makeRequest(dnnConfig, 'InitiateOAuth');
    window.open(url, '_blank');
}

async function migratePosts() {
    const result = await makeRequest(dnnConfig, 'MigratePosts', 'get', null, accessToken);
    if (result) {
        items.value = result;
    }
}

// Executed methods during the component's mounting phase
checkCode(); // Check for code in the URL
fetchItems();// Initial fetching of items
getSettings(); // Get settings
</script>