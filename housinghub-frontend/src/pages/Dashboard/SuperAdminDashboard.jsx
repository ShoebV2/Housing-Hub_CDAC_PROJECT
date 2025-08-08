import React, { useState } from 'react';
import { Building2, Users, Shield, CheckCircle, AlertCircle, Plus } from 'lucide-react';
import Card from '../../components/UI/Card';
import Button from '../../components/UI/Button';
import Table from '../../components/UI/Table';
import Modal from '../../components/UI/Modal';
import Input from '../../components/UI/Input';

const SuperAdminDashboard = () => {
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [newAdmin, setNewAdmin] = useState({
    name: '',
    email: '',
    phone: '',
    societyId: ''
  });

  const stats = [
    { icon: Building2, label: 'Total Societies', value: '12', color: 'bg-blue-500' },
    { icon: CheckCircle, label: 'Verified Societies', value: '8', color: 'bg-green-500' },
    { icon: AlertCircle, label: 'Pending Verification', value: '4', color: 'bg-yellow-500' },
    { icon: Users, label: 'Total Admins', value: '15', color: 'bg-purple-500' }
  ];

  const societies = [
    { id: 1, name: 'Green Valley Heights', admin: 'John Smith', city: 'Mumbai', verified: true },
    { id: 2, name: 'Sunrise Apartments', admin: 'Jane Doe', city: 'Pune', verified: false },
    { id: 3, name: 'Royal Gardens', admin: 'Mike Johnson', city: 'Bangalore', verified: true },
    { id: 4, name: 'Lakeside Residency', admin: 'Sarah Wilson', city: 'Hyderabad', verified: false }
  ];

  return (
    <div className="space-y-8">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-gray-900">Super Admin Dashboard</h1>
          <p className="text-gray-600 mt-2">Manage societies and administrators</p>
        </div>
        <Button onClick={() => setShowCreateModal(true)}>
          <Plus className="h-4 w-4 mr-2" />
          Add Admin
        </Button>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        {stats.map((stat, index) => {
          const Icon = stat.icon;
          return (
            <Card key={index}>
              <div className="flex items-center">
                <div className={`p-3 rounded-lg ${stat.color}`}>
                  <Icon className="h-6 w-6 text-white" />
                </div>
                <div className="ml-4">
                  <p className="text-sm font-medium text-gray-600">{stat.label}</p>
                  <p className="text-2xl font-bold text-gray-900">{stat.value}</p>
                </div>
              </div>
            </Card>
          );
        })}
      </div>

      {/* Societies Table */}
      <Card>
        <div className="flex items-center justify-between mb-6">
          <h2 className="text-xl font-semibold text-gray-900">All Societies</h2>
          <div className="flex space-x-2">
            <Button variant="secondary" size="sm">Export</Button>
            <Button variant="primary" size="sm">
              <Plus className="h-4 w-4 mr-1" />
              New Society
            </Button>
          </div>
        </div>
        
        <Table headers={['Society Name', 'Admin', 'City', 'Status', 'Actions']}>
          {societies.map((society) => (
            <tr key={society.id} className="hover:bg-gray-50">
              <td className="px-6 py-4">
                <div className="flex items-center">
                  <Building2 className="h-5 w-5 text-gray-400 mr-3" />
                  <span className="font-medium text-gray-900">{society.name}</span>
                </div>
              </td>
              <td className="px-6 py-4 text-gray-600">{society.admin}</td>
              <td className="px-6 py-4 text-gray-600">{society.city}</td>
              <td className="px-6 py-4">
                <span
                  className={`px-2 py-1 text-xs font-medium rounded-full ${
                    society.verified
                      ? 'bg-green-100 text-green-800'
                      : 'bg-yellow-100 text-yellow-800'
                  }`}
                >
                  {society.verified ? 'Verified' : 'Pending'}
                </span>
              </td>
              <td className="px-6 py-4">
                <div className="flex space-x-2">
                  <Button variant="secondary" size="sm">Edit</Button>
                  {!society.verified && (
                    <Button variant="success" size="sm">Verify</Button>
                  )}
                  <Button variant="danger" size="sm">Delete</Button>
                </div>
              </td>
            </tr>
          ))}
        </Table>
      </Card>

      {/* Create Admin Modal */}
      <Modal
        isOpen={showCreateModal}
        onClose={() => setShowCreateModal(false)}
        title="Create New Admin"
      >
        <form className="space-y-4">
          <Input
            label="Name"
            value={newAdmin.name}
            onChange={(e) => setNewAdmin({...newAdmin, name: e.target.value})}
            placeholder="Enter admin name"
          />
          <Input
            label="Email"
            type="email"
            value={newAdmin.email}
            onChange={(e) => setNewAdmin({...newAdmin, email: e.target.value})}
            placeholder="Enter admin email"
          />
          <Input
            label="Phone"
            value={newAdmin.phone}
            onChange={(e) => setNewAdmin({...newAdmin, phone: e.target.value})}
            placeholder="Enter phone number"
          />
          <Input
            label="Society ID"
            value={newAdmin.societyId}
            onChange={(e) => setNewAdmin({...newAdmin, societyId: e.target.value})}
            placeholder="Enter society ID"
          />
          <div className="flex justify-end space-x-3 pt-4">
            <Button variant="secondary" onClick={() => setShowCreateModal(false)}>
              Cancel
            </Button>
            <Button variant="primary">Create Admin</Button>
          </div>
        </form>
      </Modal>
    </div>
  );
};

export default SuperAdminDashboard;