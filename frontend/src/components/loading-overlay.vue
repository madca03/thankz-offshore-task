<template>
  <div class="loading-overlay justify-content-center align-items-center">
    <b-spinner variant="primary"/>
  </div>
</template>

<script>
  export default {
    name: "loading-overlay"
  }
</script>

<style lang="scss">
  .loading-overlay {
    position: absolute;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background-color: rgba(0, 0, 0, 0.7);
    z-index: 100;
    display: flex;
  }
</style>
