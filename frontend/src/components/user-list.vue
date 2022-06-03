<template>
  <b-card no-body header="Users List" class="user-list mt-3">
    <b-list-group flush>
      <li-user is-header/>
      <li-user v-for="user in paginatedUsers"
               :key="user.userId"
               :user="user"
               @edit="$emit('edit-user', user)"
               @delete="$emit('delete-user', user.userId)"/>
    </b-list-group>

    <div class="d-flex flex-column justify-content-center align-items-center p-3">
      <b-pagination v-model="currentPage"
                    :total-rows="userState.users.length"
                    :per-page="perPage"/>
      <div>
        <b-button @click="$emit('add-user')">Add User</b-button>
      </div>
    </div>
  </b-card>
</template>

<script>
  import LiUser from '@/components/li-user';
  import {mapGetters} from 'vuex'
  import {GET_USER_STATE} from '@/store/types';

  export default {
    name: "user-list",
    components: {LiUser},
    data() {
      return {
        user: {
          userId: '1234',
          username: 'madca03',
          name: 'Mark Agaton',
          email: 'markallenagaton03@gmail.com',
          contact: '09275808068'
        },
        currentPage: 1,
        perPage: 5
      }
    },
    computed: {
      ...mapGetters({
        userState: GET_USER_STATE
      }),
      paginatedUsers() {
        const startIdx = (this.currentPage - 1) * this.perPage
        return this.userState.users.slice(startIdx, startIdx + this.perPage)
      }
    }
  }
</script>

<style lang="scss">

</style>
