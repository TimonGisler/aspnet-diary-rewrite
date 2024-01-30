A diary application.
Currently deployed on: https://timongisler.dev/

Backend implemented with Aspnet.
Frontend written with Sveltekit.

Deployed using Docker compose and as ci cd github actions.

To start the application locally follow these steps:

1. Pull this repo
2. go into the sveltekit-frontend\diary-frontend folder and run `npm install`
3. run `npm run build` (this will generate the static sites the backend will serve) be sure to answer the prompts if any appear, e.g. "Does .... specify a file name or directory name on the target" --> press D
4. Go back to the root folder and modify the docker-compose.yml --> replace the image name placeholders with something e.g. diary:1.0
5. run `docker compose up`
6. navigate to http://localhost:5000/ to see the website.
