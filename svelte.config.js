import adapter from '@sveltejs/adapter-auto';

/** @type {import('@sveltejs/kit').Config} */
const config = {
	kit: {
		// adapter-auto only supports some environments, see https://svelte.dev/docs/kit/adapter-auto for a list.
		// If your environment is not supported, or you settled on a specific environment, switch out the adapter.
		// See https://svelte.dev/docs/kit/adapters for more information about adapters.
		adapter: adapter(),
		files: {
			assets: 'static',
			lib: 'src/client/lib',
			routes: 'src/client/routes',
			serviceWorker: 'src/client/service-worker',
			appTemplate: 'src/client/app.html'
		}
	}
};

export default config;
