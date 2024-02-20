import axios from "axios";

export function antiForgeryToken() {
    const service = window?.$?.ServicesFramework?.();
    return service?.getAntiForgeryValue() || '';
}

export
    function getConfig(dnnConfig, onSuccess) {
    const url = new URL(window.location.href);
    doFetch(dnnConfig, `${url.origin}/Item/GetConfig`, undefined, undefined, onSuccess);
}
export
    function getResx(dnnConfig, filename, onSuccess) {
    const url = new URL(window.location.href);
    doFetch(dnnConfig,
        `${url.origin}/API/UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator/Resx/GetResx?filename=${filename}`,
        undefined,
        undefined,
        onSuccess);
}
export
    async function makeRequest(dnnConfig, endpoint, method = 'get', data = null, accessToken = null) {
    const url = `${endpoint}`;
    let headers = {
        'Content-Type': 'application/json',
        moduleid: dnnConfig.moduleId,
        tabid: dnnConfig.tabId,
        RequestVerificationToken: dnnConfig.rvt
    };

    if (accessToken) {
        headers.AccessToken = accessToken;
    }

    let axiosConfig = {
        method,
        url,
        headers,
        withCredentials: true
    };

    if (data) {
        axiosConfig.data = data;
    }
    try {
        const response = await axios(axiosConfig);
        if (response.status === 200) {
            return response.data;
        }
    } catch (error) {
        console.log(error);
    }
}

function doFetch(dnnConfig, url, setOptions, data, onSuccess) {
    // default options
    let options = {
        method: 'GET',
        // headers go here
        headers: {
            'Content-Type': 'application/json',
            moduleid: dnnConfig.moduleId,
            tabid: dnnConfig.tabId,
            RequestVerificationToken: antiForgeryToken(),
        },
        body: data ? JSON.stringify(data) : null,
        credentials: 'include'
    }
    if (setOptions) {
        options = { ...options, ...setOptions }
    }
    const req = new Request(url)

    fetch(req, options)
        .then((response) => {
            if (response.status === 200) {
                return response.json()
            } else {
                return null
            }
        })
        .then((json) => {
            if (typeof (onSuccess) === 'function') {
                onSuccess(typeof (json) === 'string' ? JSON.parse(json) : json)
            }
        })
}