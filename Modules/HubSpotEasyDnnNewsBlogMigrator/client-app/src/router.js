import { createRouter, createWebHistory } from 'vue-router';
import HomeItem from './components/HomeItem.vue';
import { resolveHomePath } from './assets/utils'

const url = new URL(window.location.href);
const basePath = url.pathname;

const routes = [
    { path: resolveHomePath(basePath), component: HomeItem },
    { path: '/:pathMatch(.*)*', redirect: resolveHomePath(basePath) }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;