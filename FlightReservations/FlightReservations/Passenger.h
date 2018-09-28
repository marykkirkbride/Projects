#pragma once

#include <string>
namespace FlightReservationApp {

	class Passenger {


	public:
	Passenger() = default;
	
	Passenger(const std::string& firstName,
		const std::string& lastName,
		const std::string& emailAddress);

	Passenger(const std::string& firstName,
		const std::string& lastName,
		const std::string& emailAddress,
		const std::string& seatNumber,
		const std::string& flightNumber	);

	void display() const;
	 
	void setFirstName(const std::string& firstName);
	const std::string& getFirstName() const;

	void setLastName(const std::string& lastName);
	const std::string& getLastName() const;

	void setEmailAddress(const std::string& emailAddress);
	const std::string& getEmailAddress() const;

	void setSeatNumber(const std::string& seatNumber);
	const std::string& getSeatNumber() const;

	void setFlightNumber(const std::string& flightNumber);
	const std::string& getFlightNumber() const;

	private:

	std::string mFirstName;
	std::string mLastName;
	std::string mEmailAddress;
	std::string mSeatNumber;
	std::string mFlightNumber;
	};

}
