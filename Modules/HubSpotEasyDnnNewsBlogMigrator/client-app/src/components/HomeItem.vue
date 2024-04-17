<template>
    <div class="container">
        <div class="col-8">
            <div class="card">
                <div class="card-body">
                    <div class="mb-2">
                        <label class="text-bold">{{ resx.AuthenticationMethod }}</label>
                        <div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" type="radio" name="rAuth" id="rOAuth2" value="OAuth2"
                                    :checked="authMethod === 'OAuth2'" @change="updateAuthMethod('OAuth2')">
                                <label class="form-check-label" for="rOAuth2"> {{ resx.OAuth2 }}</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input class="form-check-input" type="radio" name="rAuth" id="rAccessToken"
                                    value="AccessToken" :checked="authMethod === 'AccessToken'"
                                    @change="updateAuthMethod('AccessToken')">
                                <label class="form-check-label" for="rAccessToken"> {{ resx.AccessToken }}</label>
                            </div>
                        </div>
                    </div>

                    <div v-if="authMethod === 'OAuth2'">
                        <h5 class="card-title">{{ resx.Welcome }}</h5>
                        <p class="card-text">{{ message }}</p>
                        <span v-if="tokenExpired">
                            <ul class="dnnActions dnnClear">
                                <li><a class="dnnPrimaryAction" @click="geturlForInitiateOAuth()">{{
                                        resx.LogHubspot }}</a></li>
                            </ul>
                        </span>
                        <span v-if="!tokenExpired">
                            <div v-if="isLoading" class="spinner"></div>
                            <h6>{{ resx.ThereAreTotalOf }} {{ items.Total }} {{ resx.PostsOnHubSpot }}</h6>
                            <div class="dnnFormMessage dnnFormSuccess col-6" v-if="showResults">
                                {{ resx.ATotalOf }} {{ postMigrated }} {{ resx.WereMigratedSuccessfully }}
                            </div>
                            <ul class="dnnActions dnnClear">
                                <li><a class="dnnPrimaryAction" @click="migratePosts(accessToken)">{{ resx.MigratePosts
                                        }}</a></li>
                            </ul>
                        </span>
                    </div>

                    <div v-else>
                        <h5 class="card-title">{{ resx.Welcome }}</h5>
                        <p class="card-text">{{ privateAccessTokenMessage }}</p>
                        <div v-if="isLoading" class="spinner"></div>
                        <span v-if="isValidPrivateAccessToken">
                            <h6>{{ resx.ThereAreTotalOf }} {{ items.Total }} {{ resx.PostsOnHubSpot }}</h6>
                            <div class="dnnFormMessage dnnFormSuccess col-6" v-if="showResults">
                                {{ resx.ATotalOf }} {{ postMigrated }} {{ resx.WereMigratedSuccessfully }}
                            </div>
                            <ul class="dnnActions dnnClear">
                                <li><a class="dnnPrimaryAction" @click="migratePosts(privateAccessToken)">{{
                                        resx.MigratePosts }}</a></li>
                            </ul>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <ul class="dnnActions dnnClear">
            <li><a class="dnnPrimaryAction" @click="getImageInSummary()">getImageInSummary</a>
            </li>
            <li><a class="dnnPrimaryAction" @click="UpdateUrlInSummary()">UpdateUrlInSummary</a>
            </li>
        </ul>
        <div class="col-4 mx-2">
            <HubSpotSettings v-if="authMethod === 'OAuth2'" />
            <HubSpotAccessTokenSettings v-else @updatePrivateAccessToken="updatePrivateAccessToken" />
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
import HubSpotAccessTokenSettings from './HubSpotAccessTokenSettings.vue';
import MigrateImage from './MigrateImage.vue';

// Injected dependencies
const resx = inject("resx");
const dnnConfig = inject("dnnConfig");

// Reactive references
const items = ref([]);
const message = ref('');
const tokenExpired = ref(false);
const postMigrated = ref(0);
const showResults = ref(false);
let isLoading = ref(false);
const authMethod = ref('OAuth2');
const privateAccessToken = ref('');
const isValidPrivateAccessToken = ref(false);
const privateAccessTokenMessage = ref('');

// Variables
const accessToken = getCookie('access_token');

// Functions

const updatePrivateAccessToken = async (newAccessToken) => {
    privateAccessToken.value = newAccessToken;
    await getBlogPosts(privateAccessToken.value)
}

const updateAuthMethod = async (method) => {
    authMethod.value = method;
    if (method === '')
        await getBlogPosts(accessToken);
    else
        await getBlogPosts(privateAccessToken.value)
}

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

const getBlogPosts = async (token) => {
    var endpoint = `${getUrlBase()}Hubspot/GetBlogPosts`
    isLoading.value = true;
    const result = await makeRequest(dnnConfig, endpoint, 'get', null, token);
    if (result) {
        if (result.Status == "error") {
            tokenExpired.value = true;
            message.value = result.Message;
            if (token === privateAccessToken.value) {
                isValidPrivateAccessToken.value = false;
                privateAccessTokenMessage.value = result.Message;
            }

        } else {
            if (token === privateAccessToken.value) {
                isValidPrivateAccessToken.value = true;
                privateAccessTokenMessage.value = '';
            }

        }
        items.value = result;
    }
    isLoading.value = false;
}
async function geturlForInitiateOAuth() {
    var endpoint = `${getUrlBase()}Hubspot/InitiateOAuth`
    var url = await makeRequest(dnnConfig, endpoint);
    window.open(url, '_blank');
}

async function migratePosts(token) {
    showResults.value = false;
    isLoading.value = true;
    var endpoint = `${getUrlBase()}Hubspot/MigratePosts`
    const result = await makeRequest(dnnConfig, endpoint, 'get', null, token);
    isLoading.value = false;
    postMigrated.value = result;
    showResults.value = true;
}
async function getImageInSummary() {
    showResults.value = false;
    isLoading.value = true;
    var endpoint = `${getUrlBase()}Hubspot/GetImageInSummary`
    const result = await makeRequest(dnnConfig, endpoint);
    isLoading.value = false;
    postMigrated.value = result;
    showResults.value = true;
}
async function UpdateUrlInSummary() {
    showResults.value = false;
    isLoading.value = true;
    var endpoint = `${getUrlBase()}Hubspot/UpdateUrlInSummary`
    const result = await makeRequest(dnnConfig, endpoint);
    isLoading.value = false;
    postMigrated.value = result;
    showResults.value = true;
}

// Executed methods during the component's mounting phase
checkCode(); // Check for code in the URL
getBlogPosts(accessToken);// Initial fetching of items
</script>