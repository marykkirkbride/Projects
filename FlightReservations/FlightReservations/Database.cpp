#include "stdafx.h"
#include "Database.h"
#include "Flight.h"
#include <iostream>
#include <stdexcept>

using namespace std;

namespace FlightReservationApp {

	Passenger&  Database::addPassenger(const string& firstName,
		const string& lastName,
		const string& emailAddress)
	{
		Passenger Passenger(firstName, lastName, emailAddress);
		mPassengers.push_back(Passenger);
		return mPassengers[mPassengers.size() - 1];
	}

	Passenger&  Database::addBooking(const string& firstName,
		const string& lastName,
		const string& emailAddress,
		const string& seatNumber,
		const string& flightNumber)
	{
		Passenger Passenger(firstName, lastName, emailAddress, seatNumber, flightNumber);
		mPassengers.push_back(Passenger);
		return mPassengers[mPassengers.size() - 1];
	}

	Passenger& Database::getPassengerInfo(const string& firstName, const string& lastName)
	{
		for (auto& passenger : mPassengers)
		{
			if (passenger.getFirstName() == firstName
				&& passenger.getLastName() == lastName)
			{
				return passenger;
			}
		}
	}

	
	void Database::displayAllPassengers() const
	{
		for (const auto& passenger : mPassengers)
		{
			passenger.display();
		}
	}
}
