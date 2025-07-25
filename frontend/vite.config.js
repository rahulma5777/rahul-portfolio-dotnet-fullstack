import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// Vite configuration for the NetFullStack front‑end.  See
// https://vitejs.dev/config/ for more options.
export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000
  }
});