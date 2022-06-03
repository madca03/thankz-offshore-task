import Vue from 'vue'
import VueRouter from 'vue-router'
import RootUsers from '../screens/root-users'
import RouterContants from '@/router/router-contants';

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: RouterContants.ROUTE_USERS,
    component: RootUsers
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
