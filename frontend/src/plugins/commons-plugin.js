import {MUTATE_SET_APP_STATE} from '@/store/types';

export default {
  install(Vue) {
    Vue.prototype.$toggleBlockingLoaderVisibility = function (val) {
      this.$store.commit(MUTATE_SET_APP_STATE, {
        showBlockingLoader: val
      })
    }
  }
}
