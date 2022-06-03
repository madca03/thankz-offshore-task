import {
  ACTION_FETCH_USERS,
  GET_USER_STATE,
  MUTATE_REMOVE_USER_FROM_LIST,
  MUTATE_SET_USER_STATE,
  MUTATE_UPDATE_USER_LIST
} from '@/store/types';
import BackendService from '@/services/backend-service';
import ObjectUtil from '@/utils/object-util'
import UserUtil from '@/utils/user-util'
import Vue from 'vue'

const state = {
  users: []
}

const mutations = {
  [MUTATE_SET_USER_STATE]: (state, payload) => {
    ObjectUtil.copyDestPropsFromSource(state, payload)
  },
  [MUTATE_UPDATE_USER_LIST]: (state, user) => {
    user.fullname = UserUtil.getFullname(user.firstname, user.middlename, user.lastname)
    const idx = state.users.findIndex(x => x.userId === user.userId)
    if (idx < 0) {
      state.users.push(user)
    } else {
      Vue.set(state.users, idx, user)
    }
  },
  [MUTATE_REMOVE_USER_FROM_LIST]: (state, userId) => {
    const idx = state.users.findIndex(x => x.userId === userId)
    if (idx < 0) return
    Vue.delete(state.users, idx)
  }
}

const actions = {
  [ACTION_FETCH_USERS]: ({commit}) => {
    return new Promise((resolve, reject) => {
      BackendService.getUsers()
        .then(users => {
          for (const u of users) {
            u.fullname = UserUtil.getFullname(u.firstname, u.middlename, u.lastname)
          }
          commit(MUTATE_SET_USER_STATE, {users})
          resolve(users)
        })
        .catch(reject)
    })
  }
}
const getters = {
  [GET_USER_STATE]: (state) => state
}

export default {
  state,
  mutations,
  actions,
  getters
}
