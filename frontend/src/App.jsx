import React from 'react';
import UserList from './components/UserList.jsx';

// Root component that composes the user list.  Additional
// components (e.g. routing, forms) could be added here.
export default function App() {
  return (
    <main style={{ padding: '1rem', maxWidth: '800px', margin: '0 auto' }}>
      <h1>NetFullStack Users</h1>
      <UserList />
    </main>
  );
}