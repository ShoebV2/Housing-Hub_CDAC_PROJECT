import React, { useState } from 'react';
import { 
  Users, 
  UserPlus, 
  Clock, 
  CheckCircle, 
  Car, 
  Search,
  Plus,
  AlertCircle
} from 'lucide-react';
import Card from '../../components/UI/Card';
import Button from '../../components/UI/Button';
import Modal from '../../components/UI/Modal';
import Input from '../../components/UI/Input';
import Table from '../../components/UI/Table';

const SecurityDashboard = () => {
  const [showVisitorModal, setShowVisitorModal] = useState(false);
  const [newVisitor, setNewVisitor] = useState({
    name: '',
    phone: '',
    flatNumber: '',
    purpose: '',
    vehicleNo: '',
    visitorType: 'guest'
  });

  const stats = [
    { icon: Users, label: 'Visitors Inside', value: '8', color: 'bg-blue-500' },
    { icon: UserPlus, label: 'Today\'s Entries', value: '23', color: 'bg-green-500' },
    { icon: Clock, label: 'Avg. Visit Duration', value: '2.5h', color: 'bg-yellow-500' },
    { icon: Car, label: 'Vehicles Parked', value: '15', color: 'bg-purple-500' }
  ];

  const currentVisitors = [
    { id: 1, name: 'John Smith', flat: 'A-301', purpose: 'Meeting', entryTime: '10:30 AM', vehicleNo: 'MH01AB1234', type: 'guest' },
    { id: 2, name: 'Sarah Delivery', flat: 'B-205', purpose: 'Package Delivery', entryTime: '11:15 AM', vehicleNo: '', type: 'delivery' },
    { id: 3, name: 'Mike Technician', flat: 'C-102', purpose: 'AC Repair', entryTime: '2:00 PM', vehicleNo: 'MH02CD5678', type: 'service' },
    { id: 4, name: 'Anna Visitor', flat: 'A-405', purpose: 'Personal Visit', entryTime: '3:30 PM', vehicleNo: '', type: 'guest' }
  ];

  const recentExits = [
    { name: 'Robert Wilson', flat: 'B-301', exitTime: '5:45 PM', duration: '3h 15m' },
    { name: 'Lisa Cleaner', flat: 'A-101', exitTime: '4:30 PM', duration: '2h 30m' },
    { name: 'Tom Plumber', flat: 'C-205', exitTime: '3:15 PM', duration: '1h 45m' }
  ];

  const handleVisitorEntry = () => {
    console.log('New visitor entry:', newVisitor);
    setShowVisitorModal(false);
    // Reset form
    setNewVisitor({
      name: '',
      phone: '',
      flatNumber: '',
      purpose: '',
      vehicleNo: '',
      visitorType: 'guest'
    });
  };

  const handleVisitorExit = (visitorId) => {
    console.log('Visitor exit:', visitorId);
  };

  return (
    <div className="space-y-8">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-gray-900">Security Dashboard</h1>
          <p className="text-gray-600 mt-2">Real-time visitor management and security monitoring</p>
        </div>
        <div className="flex space-x-3">
          <div className="flex items-center space-x-2 px-3 py-2 bg-green-50 rounded-lg">
            <div className="w-2 h-2 bg-green-500 rounded-full animate-pulse"></div>
            <span className="text-sm font-medium text-green-700">Live Updates</span>
          </div>
          <Button onClick={() => setShowVisitorModal(true)}>
            <Plus className="h-4 w-4 mr-2" />
            New Visitor Entry
          </Button>
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

      {/* Search Bar */}
      <Card padding="sm">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 h-4 w-4" />
          <input
            type="text"
            placeholder="Search visitors by name, flat number, or purpose..."
            className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
          />
        </div>
      </Card>

      {/* Current Visitors */}
      <Card>
        <div className="flex items-center justify-between mb-6">
          <div className="flex items-center space-x-3">
            <h2 className="text-xl font-semibold text-gray-900">Current Visitors</h2>
            <span className="px-2 py-1 bg-blue-100 text-blue-800 text-sm font-medium rounded-full">
              {currentVisitors.length} Inside
            </span>
          </div>
          <Button variant="secondary" size="sm">Export Log</Button>
        </div>
        
        <Table headers={['Name', 'Flat', 'Purpose', 'Entry Time', 'Vehicle', 'Type', 'Actions']}>
          {currentVisitors.map((visitor) => (
            <tr key={visitor.id} className="hover:bg-gray-50">
              <td className="px-6 py-4">
                <div className="flex items-center">
                  <div className="w-8 h-8 bg-blue-100 rounded-full flex items-center justify-center mr-3">
                    <Users className="h-4 w-4 text-blue-600" />
                  </div>
                  <span className="font-medium text-gray-900">{visitor.name}</span>
                </div>
              </td>
              <td className="px-6 py-4 font-medium text-gray-900">{visitor.flat}</td>
              <td className="px-6 py-4 text-gray-600">{visitor.purpose}</td>
              <td className="px-6 py-4 text-gray-600">{visitor.entryTime}</td>
              <td className="px-6 py-4">
                {visitor.vehicleNo ? (
                  <div className="flex items-center space-x-2">
                    <Car className="h-4 w-4 text-gray-400" />
                    <span className="text-gray-600">{visitor.vehicleNo}</span>
                  </div>
                ) : (
                  <span className="text-gray-400">No vehicle</span>
                )}
              </td>
              <td className="px-6 py-4">
                <span
                  className={`px-2 py-1 text-xs font-medium rounded-full ${
                    visitor.type === 'guest' ? 'bg-green-100 text-green-800' :
                    visitor.type === 'delivery' ? 'bg-yellow-100 text-yellow-800' :
                    'bg-purple-100 text-purple-800'
                  }`}
                >
                  {visitor.type}
                </span>
              </td>
              <td className="px-6 py-4">
                <Button 
                  variant="danger" 
                  size="sm"
                  onClick={() => handleVisitorExit(visitor.id)}
                >
                  Exit
                </Button>
              </td>
            </tr>
          ))}
        </Table>
      </Card>

      {/* Recent Exits */}
      <Card>
        <h2 className="text-xl font-semibold text-gray-900 mb-6">Recent Exits</h2>
        <div className="space-y-4">
          {recentExits.map((exit, index) => (
            <div key={index} className="flex items-center justify-between p-4 bg-gray-50 rounded-lg">
              <div className="flex items-center space-x-3">
                <div className="w-8 h-8 bg-gray-100 rounded-full flex items-center justify-center">
                  <CheckCircle className="h-4 w-4 text-gray-600" />
                </div>
                <div>
                  <p className="font-medium text-gray-900">{exit.name}</p>
                  <p className="text-sm text-gray-600">Flat: {exit.flat}</p>
                </div>
              </div>
              <div className="text-right">
                <p className="font-medium text-gray-900">{exit.exitTime}</p>
                <p className="text-sm text-gray-600">Duration: {exit.duration}</p>
              </div>
            </div>
          ))}
        </div>
      </Card>

      {/* Visitor Entry Modal */}
      <Modal
        isOpen={showVisitorModal}
        onClose={() => setShowVisitorModal(false)}
        title="New Visitor Entry"
        size="lg"
      >
        <form onSubmit={(e) => { e.preventDefault(); handleVisitorEntry(); }} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <Input
              label="Visitor Name"
              value={newVisitor.name}
              onChange={(e) => setNewVisitor({...newVisitor, name: e.target.value})}
              placeholder="Enter visitor's full name"
              required
            />
            <Input
              label="Phone Number"
              value={newVisitor.phone}
              onChange={(e) => setNewVisitor({...newVisitor, phone: e.target.value})}
              placeholder="Enter phone number"
              required
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <Input
              label="Flat Number"
              value={newVisitor.flatNumber}
              onChange={(e) => setNewVisitor({...newVisitor, flatNumber: e.target.value})}
              placeholder="e.g., A-301"
              required
            />
            <div className="space-y-2">
              <label className="block text-sm font-medium text-gray-700">Visitor Type</label>
              <select
                value={newVisitor.visitorType}
                onChange={(e) => setNewVisitor({...newVisitor, visitorType: e.target.value})}
                className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              >
                <option value="guest">Guest</option>
                <option value="delivery">Delivery</option>
                <option value="service">Service</option>
                <option value="vendor">Vendor</option>
              </select>
            </div>
          </div>

          <Input
            label="Purpose of Visit"
            value={newVisitor.purpose}
            onChange={(e) => setNewVisitor({...newVisitor, purpose: e.target.value})}
            placeholder="Brief description of visit purpose"
            required
          />

          <Input
            label="Vehicle Number (Optional)"
            value={newVisitor.vehicleNo}
            onChange={(e) => setNewVisitor({...newVisitor, vehicleNo: e.target.value})}
            placeholder="e.g., MH01AB1234"
          />

          <div className="flex justify-end space-x-3 pt-4">
            <Button variant="secondary" onClick={() => setShowVisitorModal(false)}>
              Cancel
            </Button>
            <Button type="submit" variant="primary">
              Register Entry
            </Button>
          </div>
        </form>
      </Modal>
    </div>
  );
};

export default SecurityDashboard;