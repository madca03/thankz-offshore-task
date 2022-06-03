import Vue from 'vue'
import Vuex from 'vuex'
import user from './user-state'
import app from './app-state'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {},
  getters: {},
  mutations: {},
  actions: {},
  modules: {
    user,
    app
  }
})
