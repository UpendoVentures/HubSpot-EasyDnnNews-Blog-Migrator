<template>
    <div class="card">
        <div class="card-body custom-module">
            <div class="text-center">
                <h5 class="card-title">{{ resx.HubSpotSettings }}</h5>
            </div>
            <form @submit="handleSubmit" class="dnnForm">
                <div class="dnnFormItem">
                    <label for="accessToken" class="dnnLabel">{{ resx.AccessToken }}</label>
                    <input v-model="clientAccessToken" type="password" id="accessToken" class="dnnFormInput" required />
                </div>
                <div class="dnnActions dnnClear dnnRight">
                    <button type="submit" class="dnnPrimaryAction">{{ resx.Save }}</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script setup>
import { inject, ref, getCurrentInstance} from 'vue';
import { makeRequest } from '../assets/api.js';
import { getCookie, getUrlBase } from '../assets/utils.js';

// Injected dependencies
const dnnConfig = inject("dnnConfig");
const resx = inject("resx");

const { emit } = getCurrentInstance();

let clientAccessToken = ref('');

const accessToken = getCookie('access_token');

const getSettings = async () => {
    var endpoint = `${getUrlBase()}Hubspot/GetAccessTokenSettings`
    const result = await makeRequest(dnnConfig, endpoint, 'get', null, accessToken);
    if (result) {
        clientAccessToken.value = result.AccessToken;
        emit('updatePrivateAccessToken', clientAccessToken.value);
    }
}

const handleSubmit = async (event) => {
    event.preventDefault();
    const data = {
        AccessToken: clientAccessToken.value
    };
    var endpoint = `${getUrlBase()}Hubspot/UpdateAccessTokenSettings`
    const response = await makeRequest(dnnConfig, endpoint, 'post', data);
    if (response) {
        emit('updatePrivateAccessToken', clientAccessToken.value);
    }
    console.log(response);
};

getSettings(); // Get settings
</script>
