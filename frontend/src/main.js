import Vue from 'vue'
import App from './app.vue'
import router from './router'
import store from './store'

import CommonsPlugin from '@/plugins/commons-plugin'

import {BootstrapVue, IconsPlugin} from 'bootstrap-vue'
import VueToast from 'vue-toast-notification'
import Vuelidate from 'vuelidate'

import './styles/app.scss'
import 'vue-toast-notification/dist/theme-default.css'

Vue.config.productionTip = false
Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(CommonsPlugin)
Vue.use(Vuelidate)
Vue.use(VueToast, {
  position: 'top',
  duration: 1500
})

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
