<template>
    <div class="mx-3">
        <div class="text-center">
            <h2>{{ resx.Welcome }}</h2>
            <ul class="dnnActions dnnClear">
                <li><a class="dnnPrimaryAction" href=".">Go Ahead</a></li>
            </ul>
            <ul class="dnnActions dnnClear">
                <li><a class="dnnPrimaryAction" @click="updateSettings()">Update Settings</a></li>
            </ul>
        </div>
    </div>
</template>

<script setup>
import { inject, ref, } from 'vue';
import axios from "axios";
import { getUrlBase } from "../assets/utils";

// Injected dependencies
const resx = inject("resx");
const dnnConfig = inject("dnnConfig");

// Reactive references
const items = ref([]);
var baseUrl = `${getUrlBase("Hubspot")}`;

// Variables
let accessToken = "CIGA18zYMRICAgEYturAFSCF6aceKKi-rgEyFHChQXlu9_fXOKdhOq_pzj8s9xTJOj8AIABB_wcAAAAAgAAAYHjAIAAAAAAAAAAEAAAYAAAAwMMfAAEAAACABgAAAAAAAAAAAAAAAAAAAAAAAgAIuAJCFNgc1RTjgBQdXFXYVb3qn66FuII_SgNuYTFSAFoA"
// Functions
const fetchItems = async () => {
    var url = `${baseUrl}/GetBlogPosts`;
    let axiosConfig = {
        url: url,
        headers: {
            'Content-Type': 'application/json',
            moduleid: dnnConfig.moduleId,
            tabid: dnnConfig.tabId,
            RequestVerificationToken: dnnConfig.rvt,
            AccessToken: accessToken,
        },
        withCredentials: true
    };
    axios({
        ...axiosConfig
    }).then((response) => {
        if (response.status === 200) {
            items.value = response.data;
        }
    }).catch((error) => {
        console.log(error);
    });
};

const migratePosts = async () => {
    var url = `${baseUrl}/MigratePosts`;
    let axiosConfig = {
        url: url,
        headers: {
            'Content-Type': 'application/json',
            moduleid: dnnConfig.moduleId,
            tabid: dnnConfig.tabId,
            RequestVerificationToken: dnnConfig.rvt,
            AccessToken: accessToken,
        },
        withCredentials: true
    };
    axios({
        ...axiosConfig
    }).then((response) => {
        if (response.status === 200) {
            items.value = response.data;
        }
    }).catch((error) => {
        console.log(error);
    });
};

const getSettings = async () => {
    var url = `${baseUrl}/GetSettings`;
    let axiosConfig = {
        url: url,
        headers: {
            'Content-Type': 'application/json',
            moduleid: dnnConfig.moduleId,
            tabid: dnnConfig.tabId,
            RequestVerificationToken: dnnConfig.rvt,
            AccessToken: accessToken,
        },
        withCredentials: true
    };
    axios({
        ...axiosConfig
    }).then((response) => {
        if (response.status === 200) {
            items.value = response.data;
        }
    }).catch((error) => {
        console.log(error);
    });
};

function updateSettings() {
    var data = {
        ClientId: "e7d75ea8-f8d6-416e-9817-7e3a00b00b82",
        ClientSecret: "cd7713a0-14ce-4a9d-87a5-3c156caf3f6f",
        RedirectUri: "https://localhost.dnndev.me/hubspot",
        Scope: "content"
    }
    var url = `${baseUrl}/UpdateSettings`;
    let axiosConfig = {
        method: 'post',
        url: url,
        data: data,
        headers: {
            'Content-Type': 'application/json',
            moduleid: dnnConfig.moduleId,
            tabid: dnnConfig.tabId,
            RequestVerificationToken: dnnConfig.rvt
        },
        withCredentials: true,
    };
    axios({
        ...axiosConfig
    }).then((response) => {
        if (response.status === 200) {
            fetchItems();
        }
    }).catch((error) => {
        console.log(error);
    });
}

// Executed methods during the component's mounting phase
fetchItems();// Initial fetching of items
migratePosts();
getSettings();
</script>