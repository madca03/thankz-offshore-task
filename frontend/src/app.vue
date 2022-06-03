<template>
  <div class="app">
    <b-navbar toggleable="lg" type="dark" variant="info">
      <b-navbar-brand href="#">Thankz Offshore Task</b-navbar-brand>
      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>

    <loading-overlay v-show="appState.showBlockingLoader"/>
    <router-view/>
  </div>
</template>

<script>
  import RouterConstants from '@/router/router-contants'
  import {ACTION_FETCH_USERS, GET_APP_STATE} from '@/store/types';
  import LoadingOverlay from '@/components/loading-overlay';
  import {mapGetters} from 'vuex'

  export default {
    name: 'app',
    components: {LoadingOverlay},
    computed: {
      ...mapGetters({
        appState: GET_APP_STATE
      })
    },
    created() {
      this.$toggleBlockingLoaderVisibility(true)
      this.$store.dispatch(ACTION_FETCH_USERS)
        .finally(() => {
          this.$toggleBlockingLoaderVisibility(false)
        })
    },
    usersRoute: {name: RouterConstants.ROUTE_USERS}
  }
</script>

<style lang="scss">

</style>
