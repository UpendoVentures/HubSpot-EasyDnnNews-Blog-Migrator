const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  outputDir: "../Scripts/client-app/",
  publicPath: "/DesktopModules/HubSpotEasyDnnNewsBlogMigrator/Scripts/client-app/",
  filenameHashing: false
})
