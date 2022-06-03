const { defineConfig } = require('@vue/cli-service')

module.exports = defineConfig({
  devServer: {
    port: 5000,
    proxy: {
      '/api/*': {
        target: process.env.VUE_APP_API_BASE_URL
      }
    }
  },
  transpileDependencies: true
})
