import React from 'react';
import { 
  Building2, 
  Users, 
  MessageSquare, 
  Wrench, 
  Calendar, 
  FileText,
  TrendingUp,
  AlertTriangle 
} from 'lucide-react';
// Remove the incorrect import and add a comment for correct usage
// import WingsFlatsPage from "./Admin/WingsFlatsPage";
// To use WingsFlatsPage, import from the correct path or use it in your router, not directly in AdminDashboard.
// Example: <Route path="/admin/wings-flats" element={<WingsFlatsPage />} />

import Card from "../../components/UI/Card";
import Button from "../../components/UI/Button";

const AdminDashboard = () => {
  const stats = [
    { icon: Building2, label: 'Total Flats', value: '120', color: 'bg-blue-500' },
    { icon: Users, label: 'Active Residents', value: '95', color: 'bg-green-500' },
    { icon: MessageSquare, label: 'Open Complaints', value: '8', color: 'bg-red-500' },
    { icon: Calendar, label: 'Amenity Bookings', value: '23', color: 'bg-purple-500' }
  ];

  const recentActivities = [
    { type: 'complaint', message: 'New complaint about water supply in Wing A', time: '2 hours ago' },
    { type: 'booking', message: 'Swimming pool booked by Flat 301', time: '4 hours ago' },
    { type: 'maintenance', message: 'Maintenance bill generated for January', time: '6 hours ago' },
    { type: 'resident', message: 'New resident registered in Flat 205', time: '1 day ago' }
  ];

  return (
    <div className="space-y-8">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-gray-900">Admin Dashboard</h1>
          <p className="text-gray-600 mt-2">Manage your society operations</p>
        </div>
        <div className="flex space-x-3">
          <Button variant="secondary">Export Report</Button>
          <Button variant="primary">Create Announcement</Button>
        </div>
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
        {/* Recent Activities */}
        <Card>
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-xl font-semibold text-gray-900">Recent Activities</h2>
            <Button variant="secondary" size="sm">View All</Button>
          </div>
          <div className="space-y-4">
            {recentActivities.map((activity, index) => (
              <div key={index} className="flex items-start space-x-3 p-3 bg-gray-50 rounded-lg">
                <div className={`p-2 rounded-lg ${
                  activity.type === 'complaint' ? 'bg-red-100 text-red-600' :
                  activity.type === 'booking' ? 'bg-purple-100 text-purple-600' :
                  activity.type === 'maintenance' ? 'bg-yellow-100 text-yellow-600' :
                  'bg-green-100 text-green-600'
                }`}>
                  {activity.type === 'complaint' && <AlertTriangle className="h-4 w-4" />}
                  {activity.type === 'booking' && <Calendar className="h-4 w-4" />}
                  {activity.type === 'maintenance' && <Wrench className="h-4 w-4" />}
                  {activity.type === 'resident' && <Users className="h-4 w-4" />}
                </div>
                <div className="flex-1">
                  <p className="text-sm text-gray-800">{activity.message}</p>
                  <p className="text-xs text-gray-500 mt-1">{activity.time}</p>
                </div>
              </div>
            ))}
          </div>
        </Card>

        {/* Quick Actions */}
        <Card>
          <h2 className="text-xl font-semibold text-gray-900 mb-6">Quick Actions</h2>
          <div className="grid grid-cols-2 gap-4">
            <Button variant="primary" className="h-16 flex-col">
              <Users className="h-5 w-5 mb-1" />
              Add Resident
            </Button>
            <Button variant="secondary" className="h-16 flex-col">
              <MessageSquare className="h-5 w-5 mb-1" />
              View Complaints
            </Button>
            <Button variant="success" className="h-16 flex-col">
              <FileText className="h-5 w-5 mb-1" />
              Generate Bills
            </Button>
            <Button variant="warning" className="h-16 flex-col">
              <Wrench className="h-5 w-5 mb-1" />
              Maintenance
            </Button>
          </div>
        </Card>
      </div>

      {/* Revenue Chart Placeholder */}
      <Card>
        <div className="flex items-center justify-between mb-6">
          <h2 className="text-xl font-semibold text-gray-900">Monthly Revenue</h2>
          <div className="flex items-center space-x-2">
            <TrendingUp className="h-5 w-5 text-green-600" />
            <span className="text-sm font-medium text-green-600">+12% from last month</span>
          </div>
        </div>
        <div className="h-64 bg-gray-50 rounded-lg flex items-center justify-center">
          <p className="text-gray-500">Chart will be implemented here</p>
        </div>
      </Card>
    </div>
  );
};

// Add route for admin wings & flats management
// Example for React Router v6+
// In your main App.jsx or routes file:
// <Route path="/admin/wings-flats" element={<WingsFlatsPage />} />

// If you use a sidebar or dashboard menu, add a link to /admin/wings-flats for admins.

export default AdminDashboard;