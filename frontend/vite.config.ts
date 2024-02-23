import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5293',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, '')
      },
      '/r': {
        target: 'http://localhost:5293',
        ws: true
      }
    }
  }
})
