import {GET_APP_STATE, MUTATE_SET_APP_STATE} from '@/store/types';
import ObjectUtil from '@/utils/object-util';

const state = {
  showBlockingLoader: false
}

const mutations = {
  [MUTATE_SET_APP_STATE]: (state, payload) => {
    ObjectUtil.copyDestPropsFromSource(state, payload)
  }
}

const actions = {}

const getters = {
  [GET_APP_STATE]: state => state
}

export default {
  state,
  mutations,
  actions,
  getters
}
