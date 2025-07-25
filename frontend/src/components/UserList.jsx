import React, { useEffect, useState } from 'react';
import { fetchUsers, createUser } from '../services/api.js';

/**
 * Displays a list of users and their tasks.  Provides a form to add
 * new users.  This component demonstrates basic state management and
 * side‑effects using React hooks.
 */
export default function UserList() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [newName, setNewName] = useState('');
  const [newEmail, setNewEmail] = useState('');

  useEffect(() => {
    async function loadUsers() {
      try {
        const data = await fetchUsers();
        setUsers(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    }
    loadUsers();
  }, []);

  async function handleSubmit(e) {
    e.preventDefault();
    if (!newName) return;
    try {
      const user = await createUser({ name: newName, email: newEmail });
      setUsers([...users, user]);
      setNewName('');
      setNewEmail('');
    } catch (err) {
      setError(err.message);
    }
  }

  if (loading) return <p>Loading users…</p>;
  if (error) return <p style={{ color: 'red' }}>Error: {error}</p>;

  return (
    <section>
      <form onSubmit={handleSubmit} style={{ marginBottom: '1rem' }}>
        <h2>Add a new user</h2>
        <div style={{ display: 'flex', gap: '0.5rem', flexWrap: 'wrap' }}>
          <input
            type="text"
            placeholder="Name"
            value={newName}
            onChange={e => setNewName(e.target.value)}
            required
          />
          <input
            type="email"
            placeholder="Email (optional)"
            value={newEmail}
            onChange={e => setNewEmail(e.target.value)}
          />
          <button type="submit">Add User</button>
        </div>
      </form>
      <h2>Users</h2>
      {users.length === 0 ? (
        <p>No users found.</p>
      ) : (
        users.map(user => (
          <article key={user.id} style={{ marginBottom: '1rem', padding: '0.5rem', border: '1px solid #ccc', borderRadius: '4px' }}>
            <h3>{user.name}</h3>
            {user.email && <p>Email: {user.email}</p>}
            <h4>Tasks:</h4>
            {user.taskItems && user.taskItems.length > 0 ? (
              <ul>
                {user.taskItems.map(task => (
                  <li key={task.id}>
                    {task.title} {task.isCompleted ? '(completed)' : ''}
                  </li>
                ))}
              </ul>
            ) : (
              <p>No tasks.</p>
            )}
          </article>
        ))
      )}
    </section>
  );
}