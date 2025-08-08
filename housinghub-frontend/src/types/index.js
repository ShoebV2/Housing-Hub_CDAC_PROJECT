
/**
 * @typedef {Object} User
 * @property {number} userId
 * @property {string} name
 * @property {string} email
 * @property {string} phone
 * @property {'super_admin'|'admin'|'resident'|'security_staff'} role
 * @property {number} [societyId]
 * @property {number} [flatId]
 * @property {boolean} isVerified
 */

/**
 * @typedef {Object} Society
 * @property {number} societyId
 * @property {string} name
 * @property {string} address
 * @property {string} state
 * @property {string} country
 * @property {string} city
 * @property {number} pincode
 * @property {string} createdAt
 * @property {boolean} isVerified
 */

/**
 * @typedef {Object} Wing
 * @property {number} wingId
 * @property {string} name
 * @property {number} totalFloors
 * @property {number} flatsPerFloor
 * @property {number} societyId
 */

/**
 * @typedef {Object} Flat
 * @property {number} flatId
 * @property {number} wingId
 * @property {string} flatNumber
 * @property {string} floorNumber
 * @property {number} area
 * @property {string} status
 * @property {Wing} [wing]
 */

/**
 * @typedef {Object} Amenity
 * @property {number} amenityId
 * @property {number} societyId
 * @property {string} name
 * @property {string} description
 * @property {number} hourlyRate
 * @property {number} maxCapacity
 * @property {string} openingTime
 * @property {string} closingTime
 */

/**
 * @typedef {Object} Booking
 * @property {number} bookingId
 * @property {number} userId
 * @property {number} flatId
 * @property {number} amenityId
 * @property {string} createdAt
 * @property {string} startDate
 * @property {string} endDate
 * @property {string} startTime
 * @property {string} endTime
 * @property {number} amount
 * @property {boolean} paid
 * @property {string} [transactionId]
 * @property {string} status
 * @property {Amenity} [amenity]
 * @property {Flat} [flat]
 * @property {User} [user]
 */

/**
 * @typedef {Object} Complaint
 * @property {number} complaintId
 * @property {number} flatId
 * @property {number} raisedBy
 * @property {string} title
 * @property {string} description
 * @property {string} category
 * @property {string} status
 * @property {string} createdAt
 * @property {string} [resolvedAt]
 * @property {Flat} [flat]
 * @property {User} [user]
 */

/**
 * @typedef {Object} Announcement
 * @property {number} aId
 * @property {string} title
 * @property {string} content
 * @property {string} createdAt
 * @property {number} createdBy
 * @property {number} societyId
 * @property {boolean} isGlobal
 * @property {User} [user]
 */

/**
 * @typedef {Object} Visitor
 * @property {number} visitorId
 * @property {number} flatId
 * @property {string} name
 * @property {string} visitorType
 * @property {string} phone
 * @property {string} [vehicleNo]
 * @property {string} purpose
 * @property {string} entryTime
 * @property {string} [exitTime]
 * @property {number} recordedBy
 * @property {Flat} [flat]
 * @property {User} [recordedByUser]
 */

/**
 * @typedef {Object} MaintenanceBill
 * @property {number} mbId
 * @property {number} flatId
 * @property {number} mfid
 * @property {string} periodStart
 * @property {string} periodEnd
 * @property {string} dueDate
 * @property {number} amount
 * @property {string} [paidDate]
 * @property {boolean} paid
 * @property {string} [transactionId]
 * @property {Flat} [flat]
 */

/**
 * @typedef {Object} AuthContextType
 * @property {User|null} user
 * @property {function(string, string): Promise<boolean>} login
 * @property {function(): void} logout
 * @property {function(any): Promise<boolean>} register
 * @property {boolean} loading
 */

export {};