# NetFullStack Front‑End

This folder contains the React front‑end for the NetFullStack portfolio
project.  It uses **Vite** and **React 18** for a fast development
experience and modern build system.

## Prerequisites

* [Node.js](https://nodejs.org/) (v18 or later) and npm
* The back‑end API should be running locally on port 5000 or the
  `VITE_API_BASE_URL` environment variable should be set.

## Getting Started

Navigate to the `frontend` directory and install the dependencies:

```bash
cd frontend
npm install
```

To run the development server:

```bash
npm run dev
```

The app will be available at `http://localhost:3000`.  It will
automatically fetch data from the back‑end at
`http://localhost:5000/api`.  You can override the API base URL by
creating a `.env` file in this directory and defining
`VITE_API_BASE_URL`:

```env
VITE_API_BASE_URL=http://localhost:5000/api
```

To build the app for production:

```bash
npm run build
```

The compiled files will be output to the `dist` folder.  You can
preview the production build locally using:

```bash
npm run preview
```

## Project Structure

* `public/index.html` – The entry HTML file.
* `src/main.jsx` – Entry point that mounts the React app.
* `src/App.jsx` – Root component.
* `src/components/` – Reusable UI components.
* `src/services/api.js` – Helper for making API calls.
