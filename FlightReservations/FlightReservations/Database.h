#pragma once

#include "Passenger.h"
#include "Seat.h"
#include "Flight.h"
#include <iostream>
#include <vector>


namespace FlightReservationApp {

	class Database
	{
	 public:
		Passenger& addPassenger(const std::string& firstName,
			const std::string& lastName,
			const std::string& emailAddress);

		Passenger& addBooking(const std::string& firstName,
			const std::string& lastName,
			const std::string& emailAddress,
			const std::string& seatNumber,
			const std::string& flightNumber);

		Passenger& getPassengerInfo(const std::string& firstName,
			const std::string& lastName);
		
		void displayAllPassengers() const;

	private:
		std::vector<Passenger> mPassengers;
		std::vector<Seat> mSeats;
	};


}