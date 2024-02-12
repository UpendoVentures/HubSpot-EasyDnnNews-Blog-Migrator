<template>
    <div class="card">
        <div class="card-body custom-module">
            <div class="text-center">
                <h5 class="card-title">{{ resx.HubSpotSettings }}</h5>
            </div>
            <form @submit="handleSubmit" class="dnnForm">
                <div class="dnnFormItem">
                    <label for="clientId" class="dnnLabel">{{ resx.ClientID }}</label>
                    <input v-model="clientId" type="password" id="clientId" class="dnnFormInput" required />
                </div>
                <div class="dnnFormItem">
                    <label for="clientSecret" class="dnnLabel">{{ resx.ClientSecret }}</label>
                    <input v-model="clientSecret" type="password" id="clientSecret" class="dnnFormInput" required />
                </div>
                <div class="dnnFormItem">
                    <label for="redirectUri" class="dnnLabel">{{ resx.RedirectURI }}</label>
                    <input v-model="redirectUri" type="text" id="redirectUri" class="dnnFormInput" required />
                </div>
                <div class="dnnFormItem">
                    <label for="scope" class="dnnLabel">{{ resx.Scope }}</label>
                    <input v-model="scope" type="text" id="scope" class="dnnFormInput" required />
                </div>
                <div class="dnnActions dnnClear dnnRight">
                    <button type="submit" class="dnnPrimaryAction">{{ resx.Save }}</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script setup>
import { inject, ref, } from 'vue';
import { makeRequest } from '../assets/api.js';
import { getCookie } from '../assets/utils.js';

// Injected dependencies
const dnnConfig = inject("dnnConfig");
const resx = inject("resx");

let clientId = ref('');
let clientSecret = ref('');
let redirectUri = ref('');
let scope = ref('');
const accessToken = getCookie('access_token');

const getSettings = async () => {
    const result = await makeRequest(dnnConfig, 'GetSettings', 'get', null, accessToken);
    if (result) {
        clientId.value = result.ClientId;
        clientSecret.value = result.ClientSecret;
        redirectUri.value = result.RedirectUri;
        scope.value = result.Scope;
    }
}

const handleSubmit = async (event) => {
    event.preventDefault();
    const data = {
        ClientId: clientId,
        ClientSecret: clientSecret,
        RedirectUri: redirectUri,
        Scope: scope
    };
    const response = await makeRequest(dnnConfig, 'UpdateSettings', 'post', data);
    console.log(response);
};

getSettings(); // Get settings
</script>
