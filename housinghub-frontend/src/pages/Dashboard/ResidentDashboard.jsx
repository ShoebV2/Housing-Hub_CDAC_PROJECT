import React, { useState } from 'react';
import { 
  Bell, 
  MessageSquare, 
  Calendar, 
  FileText, 
  Users, 
  CheckCircle,
  Clock,
  AlertCircle,
  Plus
} from 'lucide-react';
import Card from '../../components/UI/Card';
import Button from '../../components/UI/Button';
import Modal from '../../components/UI/Modal';
import Input from '../../components/UI/Input';

const ResidentDashboard = () => {
  const [showComplaintModal, setShowComplaintModal] = useState(false);
  const [newComplaint, setNewComplaint] = useState({
    title: '',
    description: '',
    category: 'maintenance'
  });

  const stats = [
    { icon: CheckCircle, label: 'Paid Bills', value: '2', color: 'bg-green-500' },
    { icon: Clock, label: 'Pending Bills', value: '1', color: 'bg-yellow-500' },
    { icon: MessageSquare, label: 'My Complaints', value: '3', color: 'bg-blue-500' },
    { icon: Calendar, label: 'Active Bookings', value: '1', color: 'bg-purple-500' }
  ];

  const announcements = [
    { id: 1, title: 'Water Supply Maintenance', content: 'Water supply will be interrupted tomorrow from 10 AM to 2 PM', time: '2 hours ago', priority: 'high' },
    { id: 2, title: 'Society Meeting', content: 'Monthly society meeting scheduled for this Saturday at 6 PM', time: '1 day ago', priority: 'medium' },
    { id: 3, title: 'Parking Guidelines', content: 'Please follow the new parking guidelines effective immediately', time: '3 days ago', priority: 'low' }
  ];

  const recentVisitors = [
    { name: 'John Delivery', purpose: 'Package Delivery', time: '10:30 AM', status: 'exited' },
    { name: 'Sarah Cleaner', purpose: 'Cleaning Service', time: '2:00 PM', status: 'inside' },
    { name: 'Mike Plumber', purpose: 'Maintenance', time: '4:15 PM', status: 'inside' }
  ];

  return (
    <div className="space-y-8">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-gray-900">My Dashboard</h1>
          <p className="text-gray-600 mt-2">Welcome back! Here's what's happening</p>
        </div>
        <Button onClick={() => setShowComplaintModal(true)}>
          <Plus className="h-4 w-4 mr-2" />
          Raise Complaint
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

      {/* Content Grid */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
        {/* Announcements */}
        <Card>
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-xl font-semibold text-gray-900">Latest Announcements</h2>
            <Bell className="h-5 w-5 text-gray-400" />
          </div>
          <div className="space-y-4">
            {announcements.map((announcement) => (
              <div key={announcement.id} className="p-4 bg-gray-50 rounded-lg">
                <div className="flex items-start justify-between mb-2">
                  <h3 className="font-medium text-gray-900">{announcement.title}</h3>
                  <span className={`px-2 py-1 text-xs font-medium rounded-full ${
                    announcement.priority === 'high' ? 'bg-red-100 text-red-800' :
                    announcement.priority === 'medium' ? 'bg-yellow-100 text-yellow-800' :
                    'bg-gray-100 text-gray-800'
                  }`}>
                    {announcement.priority}
                  </span>
                </div>
                <p className="text-sm text-gray-600 mb-2">{announcement.content}</p>
                <p className="text-xs text-gray-500">{announcement.time}</p>
              </div>
            ))}
          </div>
        </Card>

        {/* Recent Visitors */}
        <Card>
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-xl font-semibold text-gray-900">Recent Visitors</h2>
            <div className="flex items-center space-x-2">
              <div className="w-2 h-2 bg-green-500 rounded-full animate-pulse"></div>
              <span className="text-sm text-gray-600">Live Updates</span>
            </div>
          </div>
          <div className="space-y-4">
            {recentVisitors.map((visitor, index) => (
              <div key={index} className="flex items-center justify-between p-3 bg-gray-50 rounded-lg">
                <div className="flex items-center space-x-3">
                  <Users className="h-5 w-5 text-gray-400" />
                  <div>
                    <p className="font-medium text-gray-900">{visitor.name}</p>
                    <p className="text-sm text-gray-600">{visitor.purpose}</p>
                  </div>
                </div>
                <div className="text-right">
                  <p className="text-sm text-gray-900">{visitor.time}</p>
                  <span className={`px-2 py-1 text-xs font-medium rounded-full ${
                    visitor.status === 'inside' ? 'bg-blue-100 text-blue-800' : 'bg-gray-100 text-gray-800'
                  }`}>
                    {visitor.status}
                  </span>
                </div>
              </div>
            ))}
          </div>
        </Card>
      </div>

      {/* Quick Actions */}
      <Card>
        <h2 className="text-xl font-semibold text-gray-900 mb-6">Quick Actions</h2>
        <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
          <Button variant="primary" className="h-16 flex-col">
            <Calendar className="h-5 w-5 mb-1" />
            Book Amenity
          </Button>
          <Button variant="secondary" className="h-16 flex-col">
            <FileText className="h-5 w-5 mb-1" />
            Pay Bills
          </Button>
          <Button variant="warning" className="h-16 flex-col">
            <MessageSquare className="h-5 w-5 mb-1" />
            My Complaints
          </Button>
          <Button variant="success" className="h-16 flex-col">
            <Users className="h-5 w-5 mb-1" />
            Visitor Entry
          </Button>
        </div>
      </Card>

      {/* Complaint Modal */}
      <Modal
        isOpen={showComplaintModal}
        onClose={() => setShowComplaintModal(false)}
        title="Raise New Complaint"
      >
        <form className="space-y-4">
          <Input
            label="Title"
            value={newComplaint.title}
            onChange={(e) => setNewComplaint({...newComplaint, title: e.target.value})}
            placeholder="Brief description of the issue"
          />
          <div className="space-y-2">
            <label className="block text-sm font-medium text-gray-700">Category</label>
            <select
              value={newComplaint.category}
              onChange={(e) => setNewComplaint({...newComplaint, category: e.target.value})}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            >
              <option value="maintenance">Maintenance</option>
              <option value="plumbing">Plumbing</option>
              <option value="electrical">Electrical</option>
              <option value="security">Security</option>
              <option value="cleaning">Cleaning</option>
              <option value="other">Other</option>
            </select>
          </div>
          <div className="space-y-2">
            <label className="block text-sm font-medium text-gray-700">Description</label>
            <textarea
              value={newComplaint.description}
              onChange={(e) => setNewComplaint({...newComplaint, description: e.target.value})}
              rows={4}
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              placeholder="Detailed description of the issue..."
            />
          </div>
          <div className="flex justify-end space-x-3 pt-4">
            <Button variant="secondary" onClick={() => setShowComplaintModal(false)}>
              Cancel
            </Button>
            <Button variant="primary">Submit Complaint</Button>
          </div>
        </form>
      </Modal>
    </div>
  );
};

export default ResidentDashboard;