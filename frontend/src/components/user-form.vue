<template>
  <div class="mt-3">
    <b-card class="user-form" :header="cardHeader">
      <b-form @submit.prevent="onSubmit"
              @reset.prevent="onReset">

        <b-form-group
          id="input-group-2"
          label="Username"
          label-for="input-2">
          <b-form-input
            id="input-2"
            v-model="form.username"
            placeholder="Username"
            :error="$v.form.username.$error && !$v.form.username.required"/>
          <label error v-if="$v.form.username.$error && !$v.form.username.required">This field is required</label>
        </b-form-group>

        <b-form-group
          id="input-group-3"
          label="First Name"
          label-for="input-3">
          <b-form-input
            id="input-3"
            v-model="form.firstname"
            placeholder="First Name"
            :error="$v.form.firstname.$error && !$v.form.firstname.required"/>
          <label error v-if="$v.form.firstname.$error && !$v.form.firstname.required">This field is required</label>
        </b-form-group>

        <b-form-group
          id="input-group-4"
          label="Middle Name"
          label-for="input-4">
          <b-form-input
            id="input-4"
            v-model="form.middlename"
            placeholder="Middle Name"/>
        </b-form-group>

        <b-form-group
          id="input-group-5"
          label="Last Name"
          label-for="input-5">
          <b-form-input
            id="input-5"
            v-model="form.lastname"
            placeholder="Last Name"
            :error="$v.form.lastname.$error && !$v.form.lastname.required"/>
          <label error v-if="$v.form.firstname.$error && !$v.form.firstname.required">This field is required</label>
        </b-form-group>

        <b-form-group
          id="input-group-5"
          label="Contact Number"
          label-for="input-5">
          <b-form-input
            id="input-5"
            v-model="form.contact"
            placeholder="Contact Number"/>
        </b-form-group>

        <div class="user-form__actions d-flex">
          <b-button type="submit" variant="primary">Submit</b-button>
          <b-button type="reset" variant="danger">Reset</b-button>
          <b-button @click="$emit('cancel-form')">Cancel</b-button>
        </div>
      </b-form>
    </b-card>
    <b-card v-if="isAddUser"
            header="Add user from file"
            class="mt-3">
      <b-form @submit.prevent="onSubmitFile" @reset.prevent="onResetFile">
        <b-form-file v-model="file"
                     :state="Boolean(file)"
                     placeholder="Choose a file or drop it here..."
                     drop-placeholder="Drop file here..."/>
        <div class="mt-3 mb-3">Selected file: {{ file ? file.name : '' }}</div>

        <div class="user-form__actions d-flex">
          <b-button type="submit"
                    variant="primary"
                    :disabled="!Boolean(file)">
            Submit
          </b-button>
          <b-button type="reset"
                    variant="danger"
                    :disabled="!Boolean(file)">
            Reset
          </b-button>
          <b-button @click="$emit('cancel-form')">Cancel</b-button>
        </div>
      </b-form>

    </b-card>
  </div>

</template>

<script>
  import {required} from 'vuelidate/lib/validators'

  export default {
    name: "user-form",
    props: {
      user: {type: Object}
    },
    data() {
      return {
        form: {
          username: this.user?.username,
          firstname: this.user?.firstname,
          middlename: this.user?.middlename,
          lastname: this.user?.lastname,
          contact: this.user?.contact
        },
        file: null
      }
    },
    computed: {
      cardHeader() {
        return this.user ? 'Edit User' : 'Add User'
      },
      isAddUser() {
        return this.user === null
      }
    },
    methods: {
      onSubmitFile() {
        this.$emit('submit-add-from-file', this.file)
      },
      onSubmit() {
        this.$v.$touch()
        if (!this.$v.$error) {
          const form = {...this.form, userId: this.user?.userId}
          let event = this.user ? 'submit-edit-form' : 'submit-add-form'
          this.$emit(event, form)
        }
      },
      onResetFile() {
        this.file = null
      },
      onReset() {
        this.form.username = this.user?.username
        this.form.firstname = this.user?.firstname
        this.form.middlename = this.user?.middlename
        this.form.lastname = this.user?.lastname
        this.form.contact = this.user?.contact
        this.$v.$reset()
      }
    },
    validations: {
      form: {
        username: {required},
        firstname: {required},
        lastname: {required}
      }
    }
  }
</script>

<style lang="scss">
  .user-form {
    &__actions {
      column-gap: 5px;
    }
  }
</style>
