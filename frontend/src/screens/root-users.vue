<template>
  <b-container class="root-users">
    <component :is="activeComponent"
               :user="activeUser"
               @cancel-form="handleCancelForm"
               @submit-add-from-file="handleSubmitAddFromFile"
               @submit-add-form="handleSubmitAddForm"
               @submit-edit-form="handleSubmitEditForm"
               @add-user="handleAddUser"
               @edit-user="handleEdit"
               @delete-user="handleDelete"/>
  </b-container>
</template>

<script>
  import UserList from '@/components/user-list';
  import UserForm from '@/components/user-form';
  import BackendService from '@/services/backend-service';
  import ApiStatusCodes from '@/references/api-status-codes';
  import {MUTATE_REMOVE_USER_FROM_LIST, MUTATE_UPDATE_USER_LIST} from '@/store/types';

  export default {
    name: "root-users",
    components: {UserList},
    data() {
      return {
        activeComponent: UserList,
        activeUser: null
      }
    },
    methods: {
      handleAddUser() {
        this.activeUser = null
        this.activeComponent = UserForm
      },
      handleEdit(user) {
        this.activeUser = user
        this.activeComponent = UserForm
      },
      handleDelete(userId) {
        this.$toggleBlockingLoaderVisibility(true)
        BackendService.deleteUser(userId)
          .then(() => {
            this.$store.commit(MUTATE_REMOVE_USER_FROM_LIST, userId)
            this.$toast.success('Deleted user')
          })
          .catch(() => {
            this.$toast.error('Failed to delete user')
          })
          .finally(() => {
            this.$toggleBlockingLoaderVisibility(false)
          })
      },
      handleSubmitAddFromFile(file) {
        this.$toggleBlockingLoaderVisibility(true)
        BackendService.createUserFromFile(file)
          .then((users) => {
            for (const u of users) {
              this.$store.commit(MUTATE_UPDATE_USER_LIST, u)
            }
            this.$toast.success('Added users')
            this.activeComponent = UserList
          })
          .catch(() => {
            this.$toast.error('Failed to add users')
          })
          .finally(() => {
            this.$toggleBlockingLoaderVisibility(false)
          })
      },
      handleSubmitAddForm(user) {
        this.$toggleBlockingLoaderVisibility(true)
        BackendService.createUser(user)
          .then((newUser) => {
            this.$store.commit(MUTATE_UPDATE_USER_LIST, newUser)
            this.$toast.success('Added user')
            this.activeComponent = UserList
          })
          .catch((err) => {
            if (err.status === ApiStatusCodes.USERNAME_EXISTS) {
              this.$toast.error('Username already exists')
            } else {
              this.$toast.error('Failed to add user')
            }
          })
          .finally(() => {
            this.$toggleBlockingLoaderVisibility(false)
          })
      },
      handleSubmitEditForm(user) {
        this.$toggleBlockingLoaderVisibility(true)
        BackendService.updateUser(user)
          .then((updatedUser) => {
            this.$store.commit(MUTATE_UPDATE_USER_LIST, updatedUser)
            this.$toast.success('Updated user')
            this.activeComponent = UserList
          })
          .catch((err) => {
            if (err.status === ApiStatusCodes.USERNAME_EXISTS) {
              this.$toast.error('Username already exists')
            } else if (err.status === ApiStatusCodes.USER_NOT_FOUND) {
              this.$toast.error('User not found')
            } else {
              this.$toast.error('Failed to update user')
            }
          })
          .finally(() => {
            this.$toggleBlockingLoaderVisibility(false)
          })
      },
      handleCancelForm() {
        this.activeComponent = UserList
      }
    }
  }
</script>

<style scoped>

</style>
