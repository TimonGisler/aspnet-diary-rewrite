import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({

	server: {
        proxy: {
            '/api': 'http://localhost:5000',
        },
    },

	plugins: [sveltekit()]
});
