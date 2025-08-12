import React, { useEffect, useState } from 'react';
import { API_BASE_URL } from '../../config';
import Button from '../UI/Button';
import Input from '../UI/Input';
import { useAuth } from '../../contexts/AuthContext';

const SecurityStaffList = () => {
  const { user } = useAuth();
  const [staff, setStaff] = useState([]);
  const [form, setForm] = useState({ name: '', email: '', phone: '', password: '' });
  const [editingId, setEditingId] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [phoneError, setPhoneError] = useState('');
  const [emailError, setEmailError] = useState('');
  const [passwordError, setPasswordError] = useState('');

  const getAuthHeaders = () => {
    const token = localStorage.getItem('token');
    return {
      'Content-Type': 'application/json',
      ...(token ? { 'Authorization': `Bearer ${token}` } : {})
    };
  };

  // Fetch security staff for admin's society only
  const fetchStaff = async () => {
    setLoading(true);
    try {
      const res = await fetch(`${API_BASE_URL}/users`, {
        headers: getAuthHeaders()
      });
      if (!res.ok) throw new Error('Failed to load staff');
      const data = await res.json();
      setStaff(data.filter(s => s.role === 'security_staff' && s.societyId === user.societyId));
    } catch {
      setError('Failed to load staff');
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchStaff();
    // eslint-disable-next-line
  }, [user?.societyId]);

  const handleChange = e => {
    const { name, value } = e.target;
    if (name === 'phone') {
      if (!/^\d{0,10}$/.test(value)) return;
      setPhoneError(value.length === 10 ? '' : 'Phone number must be exactly 10 digits');
    }
    if (name === 'email') {
      setEmailError(value.includes('@') && value.endsWith('.com') ? '' : 'Email must contain @ and end with .com');
    }
    if (name === 'password') {
      const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$/;
      setPasswordError(passwordPattern.test(value)
        ? ''
        : 'Password must have 1 uppercase, 1 lowercase, 1 number, 1 special character');
    }
    setForm({ ...form, [name]: value });
  };

  const handleSubmit = async e => {
    e.preventDefault();
    setError('');
    setLoading(true);
    if (!/^\d{10}$/.test(form.phone)) {
      setError('Phone number must be exactly 10 digits');
      setLoading(false);
      return;
    }
    if (!form.email.includes('@') || !form.email.endsWith('.com')) {
      setError('Email must contain @ and end with .com');
      setLoading(false);
      return;
    }
    const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$/;
    if (!editingId && !passwordPattern.test(form.password)) {
      setError('Password must have 1 uppercase, 1 lowercase, 1 number, 1 special character');
      setLoading(false);
      return;
    }
    try {
      const payload = {
        name: form.name,
        email: form.email,
        phone: form.phone,
        password: form.password,
        role: 'security_staff',
        societyId: user.societyId
      };
      let url = `${API_BASE_URL}/users`;
      let method = 'POST';
      if (editingId) {
        url = `${API_BASE_URL}/users/${editingId}`;
        method = 'PUT';
        payload.userId = editingId;
        delete payload.password; // Don't send password on update
      }
      const res = await fetch(url, {
        method,
        headers: getAuthHeaders(),
        body: JSON.stringify(payload)
      });
      if (!res.ok) throw new Error('Failed to save staff');
      setForm({ name: '', email: '', phone: '', password: '' });
      setEditingId(null);
      fetchStaff();
    } catch {
      setError('Failed to save staff');
    }
    setLoading(false);
  };

  const handleEdit = staffMember => {
    setForm({
      name: staffMember.name,
      email: staffMember.email,
      phone: staffMember.phone,
      password: '',
    });
    setEditingId(staffMember.userId);
  };

  const handleDelete = async id => {
    if (!window.confirm('Delete this security staff member?')) return;
    setLoading(true);
    try {
      await fetch(`${API_BASE_URL}/users/${id}`, {
        method: 'DELETE',
        headers: getAuthHeaders()
      });
      fetchStaff();
    } catch {
      setError('Failed to delete staff');
    }
    setLoading(false);
  };

  return (
    <div>
      <h2 className="text-lg font-semibold mb-4">Security Staff</h2>
      <form onSubmit={handleSubmit} className="flex flex-wrap gap-2 mb-4 items-end">
        <div className="flex flex-col">
          <label htmlFor="staff-name" className="font-semibold mb-1">Name</label>
          <Input id="staff-name" name="name" value={form.name} onChange={handleChange} placeholder="Name" required />
        </div>
        <div className="flex flex-col">
          <label htmlFor="staff-email" className="font-semibold mb-1">Email</label>
          <Input id="staff-email" name="email" value={form.email} onChange={handleChange} placeholder="Email" type="email" required />
          {emailError && <span className="text-red-600 text-xs mt-1">{emailError}</span>}
        </div>
        <div className="flex flex-col">
          <label htmlFor="staff-phone" className="font-semibold mb-1">Phone</label>
          <Input id="staff-phone" name="phone" value={form.phone} onChange={handleChange} placeholder="Phone" required />
          {phoneError && <span className="text-red-600 text-xs mt-1">{phoneError}</span>}
        </div>
        {!editingId && (
          <div className="flex flex-col">
            <label htmlFor="staff-password" className="font-semibold mb-1">Password</label>
            <Input id="staff-password" name="password" value={form.password} onChange={handleChange} placeholder="Password" type="password" required />
            {passwordError && <span className="text-red-600 text-xs mt-1">{passwordError}</span>}
          </div>
        )}
        <Button type="submit" variant="primary" disabled={loading}>
          {editingId ? 'Update' : 'Add'}
        </Button>
        {editingId && (
          <Button type="button" variant="secondary" onClick={() => { setForm({ name: '', email: '', phone: '', password: '' }); setEditingId(null); }}>
            Cancel
          </Button>
        )}
      </form>
      {error && <div className="text-red-600 mb-2">{error}</div>}
      <div className="overflow-x-auto">
        <table className="min-w-full border text-sm">
          <thead>
            <tr className="bg-gray-100">
              <th className="px-4 py-2 border">Name</th>
              <th className="px-4 py-2 border">Email</th>
              <th className="px-4 py-2 border">Phone</th>
              <th className="px-4 py-2 border">Actions</th>
            </tr>
          </thead>
          <tbody>
            {staff.map(s => (
              <tr key={s.userId}>
                <td className="border px-4 py-2">{s.name}</td>
                <td className="border px-4 py-2">{s.email}</td>
                <td className="border px-4 py-2">{s.phone}</td>
                <td className="border px-4 py-2 space-x-2">
                  <Button type="button" variant="secondary" onClick={() => handleEdit(s)} size="sm">Edit</Button>
                  <Button type="button" variant="danger" onClick={() => handleDelete(s.userId)} size="sm">Delete</Button>
                </td>
              </tr>
            ))}
            {staff.length === 0 && (
              <tr><td colSpan={4} className="text-center py-4">No security staff found.</td></tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default SecurityStaffList;
