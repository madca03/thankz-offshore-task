<template>
  <b-list-group-item class="li-user"
                     :class="{'li-user--header': isHeader}">
    <div class="li-user__row">
      <template v-if="isHeader">
        <div>Username</div>
        <div>Name</div>
        <div>Contact</div>
        <div class="li-user__actions d-flex justify-content-center">
          Actions
        </div>
      </template>

      <template v-else>
        <div>{{ user.username }}</div>
        <div>{{ user.fullname }}</div>
        <div>{{ user.contact }}</div>
        <div class="li-user__actions d-flex justify-content-center">
          <b-button @click="$emit('edit')">Edit</b-button>
          <b-button @click="$emit('delete')">Delete</b-button>
        </div>
      </template>

    </div>
  </b-list-group-item>
</template>

<script>
  export default {
    name: "li-user",
    props: {
      user: {type: Object},
      isHeader: {type: Boolean, default: false}
    }
  }
</script>

<style lang="scss">
  .li-user {
    &__row {
      display: grid;
      grid-template-columns: minmax(50px, 1fr) minmax(50px, 1fr) minmax(50px, 1fr) minmax(50px, 150px);
    }

    &--header {
      background-color: #1d1d1d !important;
      color: white;
    }

    &__actions {
      column-gap: 5px;
    }
  }
</style>
