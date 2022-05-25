import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import axios from 'axios'

import data from "./js/resources/data.js"

import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'


let app = createApp(App)

app.component('Datepicker', Datepicker);
// Đặt enum  trạng thái của form
app.config.globalProperties.misaEnum = data.misaEnum
// Đặt resource api
app.config.globalProperties.misaApi = data.misaApi

// Đặt resource operator
app.config.globalProperties.misaOperator = data.misaOperator


app.use(router, axios);
app.mount('#app')
