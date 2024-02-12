/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
/******/ (function() { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/components/HomeItem.vue?vue&type=script&setup=true&lang=js":
/*!*****************************************************************************************************************************************************************************************************!*\
  !*** ./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/components/HomeItem.vue?vue&type=script&setup=true&lang=js ***!
  \*****************************************************************************************************************************************************************************************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! core-js/modules/web.url-search-params.delete.js */ \"./node_modules/core-js/modules/web.url-search-params.delete.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0__);\n/* harmony import */ var core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! core-js/modules/web.url-search-params.has.js */ \"./node_modules/core-js/modules/web.url-search-params.has.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1__);\n/* harmony import */ var core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! core-js/modules/web.url-search-params.size.js */ \"./node_modules/core-js/modules/web.url-search-params.size.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2__);\n/* harmony import */ var vue__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! vue */ \"./node_modules/vue/dist/vue.runtime.esm-bundler.js\");\n/* harmony import */ var _assets_api_js__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../assets/api.js */ \"./src/assets/api.js\");\n/* harmony import */ var _assets_utils_js__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../assets/utils.js */ \"./src/assets/utils.js\");\n\n\n\n\n\n\n\n// Injected dependencies\n\n/* harmony default export */ __webpack_exports__[\"default\"] = ({\n  __name: 'HomeItem',\n  setup(__props, {\n    expose: __expose\n  }) {\n    __expose();\n    const resx = (0,vue__WEBPACK_IMPORTED_MODULE_3__.inject)(\"resx\");\n    const dnnConfig = (0,vue__WEBPACK_IMPORTED_MODULE_3__.inject)(\"dnnConfig\");\n\n    // Reactive references\n    const items = (0,vue__WEBPACK_IMPORTED_MODULE_3__.ref)([]);\n\n    // Variables\n    const accessToken = (0,_assets_utils_js__WEBPACK_IMPORTED_MODULE_5__.getCookie)('access_token');\n\n    // Functions\n    const checkCode = async () => {\n      const url = new URL(window.location.href);\n      const params = new URLSearchParams(url.search);\n      const code = params.get('code');\n      if (code) {\n        const result = await (0,_assets_api_js__WEBPACK_IMPORTED_MODULE_4__.makeRequest)(dnnConfig, 'OAuthCallback', 'post', code, accessToken);\n        console.log(\"result\", result);\n        if (result.access_token != null) {\n          document.cookie = `access_token=${result.access_token}; path=/`;\n        }\n        // else {\n        //     document.cookie = \"access_token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;\";\n        // }\n        const url = new URL(window.location.href);\n        window.location.href = url.origin + url.pathname;\n      }\n    };\n    const fetchItems = async () => {\n      const result = await (0,_assets_api_js__WEBPACK_IMPORTED_MODULE_4__.makeRequest)(dnnConfig, 'GetBlogPosts', 'get', null, accessToken);\n      if (result) {\n        items.value = result;\n      }\n    };\n    const getSettings = async () => {\n      const result = await (0,_assets_api_js__WEBPACK_IMPORTED_MODULE_4__.makeRequest)(dnnConfig, 'GetSettings', 'get', null, accessToken);\n      if (result) {\n        items.value = result;\n      }\n    };\n    const updateSettings = async () => {\n      var data = {\n        ClientId: \"e7d75ea8-f8d6-416e-9817-7e3a00b00b82\",\n        ClientSecret: \"cd7713a0-14ce-4a9d-87a5-3c156caf3f6f\",\n        RedirectUri: \"https://localhost.dnndev.me/hubspot\",\n        Scope: \"content\"\n      };\n      const response = await (0,_assets_api_js__WEBPACK_IMPORTED_MODULE_4__.makeRequest)(dnnConfig, 'UpdateSettings', 'post', data);\n      if (response) {\n        fetchItems();\n      }\n    };\n    async function geturlForInitiateOAuth() {\n      var url = await (0,_assets_api_js__WEBPACK_IMPORTED_MODULE_4__.makeRequest)(dnnConfig, 'InitiateOAuth');\n      window.open(url, '_blank');\n    }\n    async function migratePosts() {\n      const result = await (0,_assets_api_js__WEBPACK_IMPORTED_MODULE_4__.makeRequest)(dnnConfig, 'MigratePosts', 'get', null, accessToken);\n      if (result) {\n        items.value = result;\n      }\n    }\n\n    // Executed methods during the component's mounting phase\n    checkCode(); // Check for code in the URL\n    fetchItems(); // Initial fetching of items\n    getSettings(); // Get settings\n\n    const __returned__ = {\n      resx,\n      dnnConfig,\n      items,\n      accessToken,\n      checkCode,\n      fetchItems,\n      getSettings,\n      updateSettings,\n      geturlForInitiateOAuth,\n      migratePosts,\n      inject: vue__WEBPACK_IMPORTED_MODULE_3__.inject,\n      ref: vue__WEBPACK_IMPORTED_MODULE_3__.ref,\n      get makeRequest() {\n        return _assets_api_js__WEBPACK_IMPORTED_MODULE_4__.makeRequest;\n      },\n      get getCookie() {\n        return _assets_utils_js__WEBPACK_IMPORTED_MODULE_5__.getCookie;\n      }\n    };\n    Object.defineProperty(__returned__, '__isScriptSetup', {\n      enumerable: false,\n      value: true\n    });\n    return __returned__;\n  }\n});\n\n//# sourceURL=webpack://client-app/./src/components/HomeItem.vue?./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use%5B0%5D!./node_modules/vue-loader/dist/index.js??ruleSet%5B0%5D.use%5B0%5D");

/***/ }),

/***/ "./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/templateLoader.js??ruleSet[1].rules[3]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/App.vue?vue&type=template&id=7ba5bd90":
/*!******************************************************************************************************************************************************************************************************************************************************!*\
  !*** ./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/templateLoader.js??ruleSet[1].rules[3]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/App.vue?vue&type=template&id=7ba5bd90 ***!
  \******************************************************************************************************************************************************************************************************************************************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   render: function() { return /* binding */ render; }\n/* harmony export */ });\n/* harmony import */ var vue__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! vue */ \"./node_modules/vue/dist/vue.runtime.esm-bundler.js\");\n\nfunction render(_ctx, _cache) {\n  const _component_router_view = (0,vue__WEBPACK_IMPORTED_MODULE_0__.resolveComponent)(\"router-view\");\n  return (0,vue__WEBPACK_IMPORTED_MODULE_0__.openBlock)(), (0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementBlock)(\"div\", null, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createVNode)(_component_router_view)]);\n}\n\n//# sourceURL=webpack://client-app/./src/App.vue?./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use%5B0%5D!./node_modules/vue-loader/dist/templateLoader.js??ruleSet%5B1%5D.rules%5B3%5D!./node_modules/vue-loader/dist/index.js??ruleSet%5B0%5D.use%5B0%5D");

/***/ }),

/***/ "./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/templateLoader.js??ruleSet[1].rules[3]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/components/HomeItem.vue?vue&type=template&id=b918387c":
/*!**********************************************************************************************************************************************************************************************************************************************************************!*\
  !*** ./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/templateLoader.js??ruleSet[1].rules[3]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/components/HomeItem.vue?vue&type=template&id=b918387c ***!
  \**********************************************************************************************************************************************************************************************************************************************************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   render: function() { return /* binding */ render; }\n/* harmony export */ });\n/* harmony import */ var vue__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! vue */ \"./node_modules/vue/dist/vue.runtime.esm-bundler.js\");\n\nconst _hoisted_1 = {\n  class: \"mx-3\"\n};\nconst _hoisted_2 = {\n  class: \"text-center\"\n};\nconst _hoisted_3 = {\n  class: \"dnnActions dnnClear\"\n};\nconst _hoisted_4 = {\n  class: \"dnnActions dnnClear\"\n};\nconst _hoisted_5 = {\n  class: \"dnnActions dnnClear\"\n};\nfunction render(_ctx, _cache, $props, $setup, $data, $options) {\n  return (0,vue__WEBPACK_IMPORTED_MODULE_0__.openBlock)(), (0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementBlock)(\"div\", _hoisted_1, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"div\", _hoisted_2, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"h2\", null, (0,vue__WEBPACK_IMPORTED_MODULE_0__.toDisplayString)($setup.resx.Welcome), 1 /* TEXT */), (0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"ul\", _hoisted_3, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"li\", null, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"a\", {\n    class: \"dnnPrimaryAction\",\n    onClick: _cache[0] || (_cache[0] = $event => $setup.migratePosts())\n  }, \"Migrate Posts\")])]), (0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"ul\", _hoisted_4, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"li\", null, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"a\", {\n    class: \"dnnPrimaryAction\",\n    onClick: _cache[1] || (_cache[1] = $event => $setup.updateSettings())\n  }, \"Update Settings\")])]), (0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"ul\", _hoisted_5, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"li\", null, [(0,vue__WEBPACK_IMPORTED_MODULE_0__.createElementVNode)(\"a\", {\n    class: \"dnnPrimaryAction\",\n    onClick: _cache[2] || (_cache[2] = $event => $setup.geturlForInitiateOAuth())\n  }, \"Initiate OAuth\")])])])]);\n}\n\n//# sourceURL=webpack://client-app/./src/components/HomeItem.vue?./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use%5B0%5D!./node_modules/vue-loader/dist/templateLoader.js??ruleSet%5B1%5D.rules%5B3%5D!./node_modules/vue-loader/dist/index.js??ruleSet%5B0%5D.use%5B0%5D");

/***/ }),

/***/ "./src/assets/api.js":
/*!***************************!*\
  !*** ./src/assets/api.js ***!
  \***************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   antiForgeryToken: function() { return /* binding */ antiForgeryToken; },\n/* harmony export */   getConfig: function() { return /* binding */ getConfig; },\n/* harmony export */   getResx: function() { return /* binding */ getResx; },\n/* harmony export */   makeRequest: function() { return /* binding */ makeRequest; }\n/* harmony export */ });\n/* harmony import */ var core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! core-js/modules/web.url-search-params.delete.js */ \"./node_modules/core-js/modules/web.url-search-params.delete.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0__);\n/* harmony import */ var core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! core-js/modules/web.url-search-params.has.js */ \"./node_modules/core-js/modules/web.url-search-params.has.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1__);\n/* harmony import */ var core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! core-js/modules/web.url-search-params.size.js */ \"./node_modules/core-js/modules/web.url-search-params.size.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2__);\n/* harmony import */ var axios__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! axios */ \"./node_modules/axios/lib/axios.js\");\n/* harmony import */ var _assets_utils__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../assets/utils */ \"./src/assets/utils.js\");\n\n\n\n\n\nvar baseUrl = `${(0,_assets_utils__WEBPACK_IMPORTED_MODULE_3__.getUrlBase)(\"Hubspot\")}`;\nfunction antiForgeryToken() {\n  const service = window?.$?.ServicesFramework?.();\n  return service?.getAntiForgeryValue() || '';\n}\nfunction getConfig(dnnConfig, onSuccess) {\n  const url = new URL(window.location.href);\n  doFetch(dnnConfig, `${url.origin}/Item/GetConfig`, undefined, undefined, onSuccess);\n}\nfunction getResx(dnnConfig, filename, onSuccess) {\n  const url = new URL(window.location.href);\n  doFetch(dnnConfig, `${url.origin}/API/UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator/Resx/GetResx?filename=${filename}`, undefined, undefined, onSuccess);\n}\nasync function makeRequest(dnnConfig, endpoint, method = 'get', data = null, accessToken = null) {\n  const url = `${baseUrl}/${endpoint}`;\n  let headers = {\n    'Content-Type': 'application/json',\n    moduleid: dnnConfig.moduleId,\n    tabid: dnnConfig.tabId,\n    RequestVerificationToken: dnnConfig.rvt\n  };\n  if (accessToken) {\n    headers.AccessToken = accessToken;\n  }\n  let axiosConfig = {\n    method,\n    url,\n    headers,\n    withCredentials: true\n  };\n  if (data) {\n    axiosConfig.data = data;\n  }\n  try {\n    const response = await (0,axios__WEBPACK_IMPORTED_MODULE_4__[\"default\"])(axiosConfig);\n    if (response.status === 200) {\n      return response.data;\n    }\n  } catch (error) {\n    console.log(error);\n  }\n}\nfunction doFetch(dnnConfig, url, setOptions, data, onSuccess) {\n  // default options\n  let options = {\n    method: 'GET',\n    // headers go here\n    headers: {\n      'Content-Type': 'application/json',\n      moduleid: dnnConfig.moduleId,\n      tabid: dnnConfig.tabId,\n      RequestVerificationToken: antiForgeryToken()\n    },\n    body: data ? JSON.stringify(data) : null,\n    credentials: 'include'\n  };\n  if (setOptions) {\n    options = {\n      ...options,\n      ...setOptions\n    };\n  }\n  const req = new Request(url);\n  fetch(req, options).then(response => {\n    if (response.status === 200) {\n      return response.json();\n    } else {\n      return null;\n    }\n  }).then(json => {\n    if (typeof onSuccess === 'function') {\n      onSuccess(typeof json === 'string' ? JSON.parse(json) : json);\n    }\n  });\n}\n\n//# sourceURL=webpack://client-app/./src/assets/api.js?");

/***/ }),

/***/ "./src/assets/utils.js":
/*!*****************************!*\
  !*** ./src/assets/utils.js ***!
  \*****************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   getCookie: function() { return /* binding */ getCookie; },\n/* harmony export */   getUrlBase: function() { return /* binding */ getUrlBase; },\n/* harmony export */   goHome: function() { return /* binding */ goHome; },\n/* harmony export */   resolveHomePath: function() { return /* binding */ resolveHomePath; },\n/* harmony export */   resolvePath: function() { return /* binding */ resolvePath; }\n/* harmony export */ });\n/* harmony import */ var core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! core-js/modules/web.url-search-params.delete.js */ \"./node_modules/core-js/modules/web.url-search-params.delete.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0__);\n/* harmony import */ var core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! core-js/modules/web.url-search-params.has.js */ \"./node_modules/core-js/modules/web.url-search-params.has.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1__);\n/* harmony import */ var core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! core-js/modules/web.url-search-params.size.js */ \"./node_modules/core-js/modules/web.url-search-params.size.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2__);\n\n\n\n// List of routes to be excluded for redirection purposes.\nconst excludeRoutes = ['/details', '/about'];\nconst url = new URL(window.location.href);\n\n// This function adjusts the basePath by removing routes specified in excludeRoutes to achieve the redirect to the main or home page.\nfunction resolveHomePath(basePath) {\n  let tempBase = basePath;\n  // Iterate through each element in excludeRoutes to remove them from the end of basePath.\n  excludeRoutes.forEach(element => {\n    if (tempBase.endsWith(element)) {\n      tempBase = tempBase.slice(0, -element.length);\n    }\n  });\n  return tempBase;\n}\n\n// This function ensures a path is appended to the basePath if it's not already included.\nfunction resolvePath(basePath, path) {\n  excludeRoutes.forEach(element => {\n    if (basePath.endsWith(element)) {\n      basePath = basePath.slice(0, -element.length);\n    }\n  });\n  // Checks if basePath already includes the path; if not, appends it to basePath.\n  var result = basePath.includes(path) ? basePath : `${basePath}${path}`;\n  return result;\n}\nfunction goHome() {\n  // List of routes to be excluded for redirection purposes.\n  let tempBase = url.pathname;\n  // Iterate through each element in excludeRoutes to remove them from the end of basePath.\n  excludeRoutes.forEach(element => {\n    if (tempBase.endsWith(element)) {\n      tempBase = tempBase.slice(0, -element.length);\n    }\n  });\n  return tempBase;\n}\nfunction getUrlBase(serviceName) {\n  const url = new URL(window.location.href);\n  return `${url.origin}/API/UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator/${serviceName}`;\n}\nfunction getCookie(name) {\n  const value = `; ${document.cookie}`;\n  const parts = value.split(`; ${name}=`);\n  if (parts.length === 2) return parts.pop().split(';').shift();\n  return '';\n}\n\n//# sourceURL=webpack://client-app/./src/assets/utils.js?");

/***/ }),

/***/ "./src/main.js":
/*!*********************!*\
  !*** ./src/main.js ***!
  \*********************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var vue__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! vue */ \"./node_modules/vue/dist/vue.runtime.esm-bundler.js\");\n/* harmony import */ var _App_vue__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./App.vue */ \"./src/App.vue\");\n/* harmony import */ var _assets_api__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./assets/api */ \"./src/assets/api.js\");\n/* harmony import */ var _router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./router */ \"./src/router.js\");\n/* harmony import */ var _store_index__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./store/index */ \"./src/store/index.js\");\n\n\n\n\n\nconst allAppElements = document.getElementsByClassName(\"appModule\");\nconst app = (0,vue__WEBPACK_IMPORTED_MODULE_0__.createApp)(_App_vue__WEBPACK_IMPORTED_MODULE_1__[\"default\"]);\napp.use(_router__WEBPACK_IMPORTED_MODULE_3__[\"default\"]);\napp.use(_store_index__WEBPACK_IMPORTED_MODULE_4__[\"default\"]);\napp.config.devtools = true;\nfunction getResxPromise(dnnConfig, resxKey) {\n  return new Promise(resolve => {\n    (0,_assets_api__WEBPACK_IMPORTED_MODULE_2__.getResx)(dnnConfig, resxKey, resolve);\n  });\n}\ndocument.addEventListener(\"DOMContentLoaded\", function setApp() {\n  for (var i = 0; i < allAppElements.length; i++) {\n    const thisAppElm = allAppElements[i];\n    const dnnConfig = {\n      tabId: Number(thisAppElm.getAttribute(\"data-tabid\")),\n      moduleId: Number(thisAppElm.getAttribute(\"data-moduleid\")),\n      editMode: thisAppElm.getAttribute(\"data-editmode\").toLowerCase() === \"true\",\n      apiBaseUrl: thisAppElm.getAttribute(\"data-apibaseurl\"),\n      rvt: (0,_assets_api__WEBPACK_IMPORTED_MODULE_2__.antiForgeryToken)()\n    };\n    if (window.dtCallBacks === undefined) {\n      window.dtCallBacks = [];\n    }\n    getResxPromise(dnnConfig, \"View\").then(resx => {\n      app.provide(\"dnnConfig\", dnnConfig);\n      app.provide(\"resx\", resx);\n      app.provide(\"window\", window);\n      app.provide(\"jQuery\", window.$);\n      app.mount(`#${thisAppElm.id}`);\n    }).catch(error => {\n      console.log(error);\n    });\n  }\n});\n\n//# sourceURL=webpack://client-app/./src/main.js?");

/***/ }),

/***/ "./src/router.js":
/*!***********************!*\
  !*** ./src/router.js ***!
  \***********************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! core-js/modules/web.url-search-params.delete.js */ \"./node_modules/core-js/modules/web.url-search-params.delete.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_delete_js__WEBPACK_IMPORTED_MODULE_0__);\n/* harmony import */ var core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! core-js/modules/web.url-search-params.has.js */ \"./node_modules/core-js/modules/web.url-search-params.has.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_has_js__WEBPACK_IMPORTED_MODULE_1__);\n/* harmony import */ var core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! core-js/modules/web.url-search-params.size.js */ \"./node_modules/core-js/modules/web.url-search-params.size.js\");\n/* harmony import */ var core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(core_js_modules_web_url_search_params_size_js__WEBPACK_IMPORTED_MODULE_2__);\n/* harmony import */ var vue_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! vue-router */ \"./node_modules/vue-router/dist/vue-router.mjs\");\n/* harmony import */ var _components_HomeItem_vue__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/HomeItem.vue */ \"./src/components/HomeItem.vue\");\n/* harmony import */ var _assets_utils__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./assets/utils */ \"./src/assets/utils.js\");\n\n\n\n\n\n\nconst url = new URL(window.location.href);\nconst basePath = url.pathname;\nconst routes = [{\n  path: (0,_assets_utils__WEBPACK_IMPORTED_MODULE_4__.resolveHomePath)(basePath),\n  component: _components_HomeItem_vue__WEBPACK_IMPORTED_MODULE_3__[\"default\"]\n}, {\n  path: '/:pathMatch(.*)*',\n  redirect: (0,_assets_utils__WEBPACK_IMPORTED_MODULE_4__.resolveHomePath)(basePath)\n}];\nconst router = (0,vue_router__WEBPACK_IMPORTED_MODULE_5__.createRouter)({\n  history: (0,vue_router__WEBPACK_IMPORTED_MODULE_5__.createWebHistory)(),\n  routes\n});\n/* harmony default export */ __webpack_exports__[\"default\"] = (router);\n\n//# sourceURL=webpack://client-app/./src/router.js?");

/***/ }),

/***/ "./src/store/index.js":
/*!****************************!*\
  !*** ./src/store/index.js ***!
  \****************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var vuex__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! vuex */ \"./node_modules/vuex/dist/vuex.esm-bundler.js\");\n\n/* harmony default export */ __webpack_exports__[\"default\"] = ((0,vuex__WEBPACK_IMPORTED_MODULE_0__.createStore)({\n  state: {},\n  mutations: {},\n  actions: {},\n  modules: {}\n}));\n\n//# sourceURL=webpack://client-app/./src/store/index.js?");

/***/ }),

/***/ "./src/App.vue":
/*!*********************!*\
  !*** ./src/App.vue ***!
  \*********************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var _App_vue_vue_type_template_id_7ba5bd90__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./App.vue?vue&type=template&id=7ba5bd90 */ \"./src/App.vue?vue&type=template&id=7ba5bd90\");\n/* harmony import */ var _node_modules_vue_loader_dist_exportHelper_js__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../node_modules/vue-loader/dist/exportHelper.js */ \"./node_modules/vue-loader/dist/exportHelper.js\");\n\nconst script = {}\n\n;\nconst __exports__ = /*#__PURE__*/(0,_node_modules_vue_loader_dist_exportHelper_js__WEBPACK_IMPORTED_MODULE_1__[\"default\"])(script, [['render',_App_vue_vue_type_template_id_7ba5bd90__WEBPACK_IMPORTED_MODULE_0__.render],['__file',\"src/App.vue\"]])\n/* hot reload */\nif (false) {}\n\n\n/* harmony default export */ __webpack_exports__[\"default\"] = (__exports__);\n\n//# sourceURL=webpack://client-app/./src/App.vue?");

/***/ }),

/***/ "./src/components/HomeItem.vue":
/*!*************************************!*\
  !*** ./src/components/HomeItem.vue ***!
  \*************************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var _HomeItem_vue_vue_type_template_id_b918387c__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./HomeItem.vue?vue&type=template&id=b918387c */ \"./src/components/HomeItem.vue?vue&type=template&id=b918387c\");\n/* harmony import */ var _HomeItem_vue_vue_type_script_setup_true_lang_js__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./HomeItem.vue?vue&type=script&setup=true&lang=js */ \"./src/components/HomeItem.vue?vue&type=script&setup=true&lang=js\");\n/* harmony import */ var _node_modules_vue_loader_dist_exportHelper_js__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../node_modules/vue-loader/dist/exportHelper.js */ \"./node_modules/vue-loader/dist/exportHelper.js\");\n\n\n\n\n;\nconst __exports__ = /*#__PURE__*/(0,_node_modules_vue_loader_dist_exportHelper_js__WEBPACK_IMPORTED_MODULE_2__[\"default\"])(_HomeItem_vue_vue_type_script_setup_true_lang_js__WEBPACK_IMPORTED_MODULE_1__[\"default\"], [['render',_HomeItem_vue_vue_type_template_id_b918387c__WEBPACK_IMPORTED_MODULE_0__.render],['__file',\"src/components/HomeItem.vue\"]])\n/* hot reload */\nif (false) {}\n\n\n/* harmony default export */ __webpack_exports__[\"default\"] = (__exports__);\n\n//# sourceURL=webpack://client-app/./src/components/HomeItem.vue?");

/***/ }),

/***/ "./src/components/HomeItem.vue?vue&type=script&setup=true&lang=js":
/*!************************************************************************!*\
  !*** ./src/components/HomeItem.vue?vue&type=script&setup=true&lang=js ***!
  \************************************************************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   \"default\": function() { return /* reexport safe */ _node_modules_babel_loader_lib_index_js_clonedRuleSet_40_use_0_node_modules_vue_loader_dist_index_js_ruleSet_0_use_0_HomeItem_vue_vue_type_script_setup_true_lang_js__WEBPACK_IMPORTED_MODULE_0__[\"default\"]; }\n/* harmony export */ });\n/* harmony import */ var _node_modules_babel_loader_lib_index_js_clonedRuleSet_40_use_0_node_modules_vue_loader_dist_index_js_ruleSet_0_use_0_HomeItem_vue_vue_type_script_setup_true_lang_js__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! -!../../node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!../../node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./HomeItem.vue?vue&type=script&setup=true&lang=js */ \"./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/components/HomeItem.vue?vue&type=script&setup=true&lang=js\");\n \n\n//# sourceURL=webpack://client-app/./src/components/HomeItem.vue?");

/***/ }),

/***/ "./src/App.vue?vue&type=template&id=7ba5bd90":
/*!***************************************************!*\
  !*** ./src/App.vue?vue&type=template&id=7ba5bd90 ***!
  \***************************************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   render: function() { return /* reexport safe */ _node_modules_babel_loader_lib_index_js_clonedRuleSet_40_use_0_node_modules_vue_loader_dist_templateLoader_js_ruleSet_1_rules_3_node_modules_vue_loader_dist_index_js_ruleSet_0_use_0_App_vue_vue_type_template_id_7ba5bd90__WEBPACK_IMPORTED_MODULE_0__.render; }\n/* harmony export */ });\n/* harmony import */ var _node_modules_babel_loader_lib_index_js_clonedRuleSet_40_use_0_node_modules_vue_loader_dist_templateLoader_js_ruleSet_1_rules_3_node_modules_vue_loader_dist_index_js_ruleSet_0_use_0_App_vue_vue_type_template_id_7ba5bd90__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! -!../node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!../node_modules/vue-loader/dist/templateLoader.js??ruleSet[1].rules[3]!../node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./App.vue?vue&type=template&id=7ba5bd90 */ \"./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/templateLoader.js??ruleSet[1].rules[3]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/App.vue?vue&type=template&id=7ba5bd90\");\n\n\n//# sourceURL=webpack://client-app/./src/App.vue?");

/***/ }),

/***/ "./src/components/HomeItem.vue?vue&type=template&id=b918387c":
/*!*******************************************************************!*\
  !*** ./src/components/HomeItem.vue?vue&type=template&id=b918387c ***!
  \*******************************************************************/
/***/ (function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {

eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export */ __webpack_require__.d(__webpack_exports__, {\n/* harmony export */   render: function() { return /* reexport safe */ _node_modules_babel_loader_lib_index_js_clonedRuleSet_40_use_0_node_modules_vue_loader_dist_templateLoader_js_ruleSet_1_rules_3_node_modules_vue_loader_dist_index_js_ruleSet_0_use_0_HomeItem_vue_vue_type_template_id_b918387c__WEBPACK_IMPORTED_MODULE_0__.render; }\n/* harmony export */ });\n/* harmony import */ var _node_modules_babel_loader_lib_index_js_clonedRuleSet_40_use_0_node_modules_vue_loader_dist_templateLoader_js_ruleSet_1_rules_3_node_modules_vue_loader_dist_index_js_ruleSet_0_use_0_HomeItem_vue_vue_type_template_id_b918387c__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! -!../../node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!../../node_modules/vue-loader/dist/templateLoader.js??ruleSet[1].rules[3]!../../node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./HomeItem.vue?vue&type=template&id=b918387c */ \"./node_modules/babel-loader/lib/index.js??clonedRuleSet-40.use[0]!./node_modules/vue-loader/dist/templateLoader.js??ruleSet[1].rules[3]!./node_modules/vue-loader/dist/index.js??ruleSet[0].use[0]!./src/components/HomeItem.vue?vue&type=template&id=b918387c\");\n\n\n//# sourceURL=webpack://client-app/./src/components/HomeItem.vue?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = __webpack_modules__;
/******/ 	
/************************************************************************/
/******/ 	/* webpack/runtime/chunk loaded */
/******/ 	!function() {
/******/ 		var deferred = [];
/******/ 		__webpack_require__.O = function(result, chunkIds, fn, priority) {
/******/ 			if(chunkIds) {
/******/ 				priority = priority || 0;
/******/ 				for(var i = deferred.length; i > 0 && deferred[i - 1][2] > priority; i--) deferred[i] = deferred[i - 1];
/******/ 				deferred[i] = [chunkIds, fn, priority];
/******/ 				return;
/******/ 			}
/******/ 			var notFulfilled = Infinity;
/******/ 			for (var i = 0; i < deferred.length; i++) {
/******/ 				var chunkIds = deferred[i][0];
/******/ 				var fn = deferred[i][1];
/******/ 				var priority = deferred[i][2];
/******/ 				var fulfilled = true;
/******/ 				for (var j = 0; j < chunkIds.length; j++) {
/******/ 					if ((priority & 1 === 0 || notFulfilled >= priority) && Object.keys(__webpack_require__.O).every(function(key) { return __webpack_require__.O[key](chunkIds[j]); })) {
/******/ 						chunkIds.splice(j--, 1);
/******/ 					} else {
/******/ 						fulfilled = false;
/******/ 						if(priority < notFulfilled) notFulfilled = priority;
/******/ 					}
/******/ 				}
/******/ 				if(fulfilled) {
/******/ 					deferred.splice(i--, 1)
/******/ 					var r = fn();
/******/ 					if (r !== undefined) result = r;
/******/ 				}
/******/ 			}
/******/ 			return result;
/******/ 		};
/******/ 	}();
/******/ 	
/******/ 	/* webpack/runtime/compat get default export */
/******/ 	!function() {
/******/ 		// getDefaultExport function for compatibility with non-harmony modules
/******/ 		__webpack_require__.n = function(module) {
/******/ 			var getter = module && module.__esModule ?
/******/ 				function() { return module['default']; } :
/******/ 				function() { return module; };
/******/ 			__webpack_require__.d(getter, { a: getter });
/******/ 			return getter;
/******/ 		};
/******/ 	}();
/******/ 	
/******/ 	/* webpack/runtime/define property getters */
/******/ 	!function() {
/******/ 		// define getter functions for harmony exports
/******/ 		__webpack_require__.d = function(exports, definition) {
/******/ 			for(var key in definition) {
/******/ 				if(__webpack_require__.o(definition, key) && !__webpack_require__.o(exports, key)) {
/******/ 					Object.defineProperty(exports, key, { enumerable: true, get: definition[key] });
/******/ 				}
/******/ 			}
/******/ 		};
/******/ 	}();
/******/ 	
/******/ 	/* webpack/runtime/global */
/******/ 	!function() {
/******/ 		__webpack_require__.g = (function() {
/******/ 			if (typeof globalThis === 'object') return globalThis;
/******/ 			try {
/******/ 				return this || new Function('return this')();
/******/ 			} catch (e) {
/******/ 				if (typeof window === 'object') return window;
/******/ 			}
/******/ 		})();
/******/ 	}();
/******/ 	
/******/ 	/* webpack/runtime/hasOwnProperty shorthand */
/******/ 	!function() {
/******/ 		__webpack_require__.o = function(obj, prop) { return Object.prototype.hasOwnProperty.call(obj, prop); }
/******/ 	}();
/******/ 	
/******/ 	/* webpack/runtime/make namespace object */
/******/ 	!function() {
/******/ 		// define __esModule on exports
/******/ 		__webpack_require__.r = function(exports) {
/******/ 			if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 				Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 			}
/******/ 			Object.defineProperty(exports, '__esModule', { value: true });
/******/ 		};
/******/ 	}();
/******/ 	
/******/ 	/* webpack/runtime/jsonp chunk loading */
/******/ 	!function() {
/******/ 		// no baseURI
/******/ 		
/******/ 		// object to store loaded and loading chunks
/******/ 		// undefined = chunk not loaded, null = chunk preloaded/prefetched
/******/ 		// [resolve, reject, Promise] = chunk loading, 0 = chunk loaded
/******/ 		var installedChunks = {
/******/ 			"app": 0
/******/ 		};
/******/ 		
/******/ 		// no chunk on demand loading
/******/ 		
/******/ 		// no prefetching
/******/ 		
/******/ 		// no preloaded
/******/ 		
/******/ 		// no HMR
/******/ 		
/******/ 		// no HMR manifest
/******/ 		
/******/ 		__webpack_require__.O.j = function(chunkId) { return installedChunks[chunkId] === 0; };
/******/ 		
/******/ 		// install a JSONP callback for chunk loading
/******/ 		var webpackJsonpCallback = function(parentChunkLoadingFunction, data) {
/******/ 			var chunkIds = data[0];
/******/ 			var moreModules = data[1];
/******/ 			var runtime = data[2];
/******/ 			// add "moreModules" to the modules object,
/******/ 			// then flag all "chunkIds" as loaded and fire callback
/******/ 			var moduleId, chunkId, i = 0;
/******/ 			if(chunkIds.some(function(id) { return installedChunks[id] !== 0; })) {
/******/ 				for(moduleId in moreModules) {
/******/ 					if(__webpack_require__.o(moreModules, moduleId)) {
/******/ 						__webpack_require__.m[moduleId] = moreModules[moduleId];
/******/ 					}
/******/ 				}
/******/ 				if(runtime) var result = runtime(__webpack_require__);
/******/ 			}
/******/ 			if(parentChunkLoadingFunction) parentChunkLoadingFunction(data);
/******/ 			for(;i < chunkIds.length; i++) {
/******/ 				chunkId = chunkIds[i];
/******/ 				if(__webpack_require__.o(installedChunks, chunkId) && installedChunks[chunkId]) {
/******/ 					installedChunks[chunkId][0]();
/******/ 				}
/******/ 				installedChunks[chunkId] = 0;
/******/ 			}
/******/ 			return __webpack_require__.O(result);
/******/ 		}
/******/ 		
/******/ 		var chunkLoadingGlobal = self["webpackChunkclient_app"] = self["webpackChunkclient_app"] || [];
/******/ 		chunkLoadingGlobal.forEach(webpackJsonpCallback.bind(null, 0));
/******/ 		chunkLoadingGlobal.push = webpackJsonpCallback.bind(null, chunkLoadingGlobal.push.bind(chunkLoadingGlobal));
/******/ 	}();
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module depends on other loaded chunks and execution need to be delayed
/******/ 	var __webpack_exports__ = __webpack_require__.O(undefined, ["chunk-vendors"], function() { return __webpack_require__("./src/main.js"); })
/******/ 	__webpack_exports__ = __webpack_require__.O(__webpack_exports__);
/******/ 	
/******/ })()
;