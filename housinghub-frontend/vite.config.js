import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// Enable proxy to backend for API calls during development
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:44371',
        changeOrigin: true,
        secure: false
      }
    }
  }
});
