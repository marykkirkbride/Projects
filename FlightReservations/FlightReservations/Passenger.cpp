#include "stdafx.h"
#include "Passenger.h"
#include <iostream>

using namespace std;

namespace FlightReservationApp {

	Passenger::Passenger(const string& firstName,
		const string& lastName,
		const string& emailAddress)
		: mFirstName(firstName), mLastName(lastName), mEmailAddress(emailAddress)
		{
		}

	Passenger::Passenger(const std::string& firstName,
		const std::string& lastName,
		const std::string& emailAddress,
		const std::string& seatNumber,
		const std::string& flightNumber)
		: mFirstName(firstName), mLastName(lastName), mEmailAddress(emailAddress), mSeatNumber(seatNumber), mFlightNumber(flightNumber)
		{
		}

	void Passenger::setFirstName(const string& firstName)
	{
		mFirstName = firstName;
	}

	const string& Passenger::getFirstName() const 
	{
		return mFirstName;
	}

	void Passenger::setLastName(const string& lastName)
	{
		mLastName = lastName;
	}

	const string& Passenger::getLastName() const
	{
		return mLastName;
	}

	void Passenger::setEmailAddress(const string& emailAddress) 
	{
		mEmailAddress = emailAddress;
	}

	const string& Passenger::getEmailAddress() const
	{
		return mEmailAddress;
	}

	void Passenger::setSeatNumber(const string& seatNumber)
	{
		mSeatNumber = seatNumber;
	}

	const string& Passenger::getSeatNumber() const
	{
		return mSeatNumber;
	}

	void Passenger::setFlightNumber(const string& flightNumber)
	{
		mFlightNumber = flightNumber;
	}

	const string& Passenger::getFlightNumber() const
	{
		return mFlightNumber;
	}

	void Passenger::display() const
	{
		cout << "Passenger Name: " << getFirstName() << " " << getLastName() << endl;
		cout << "Contact Information: " << getEmailAddress() << endl;
		cout << "Seat Number:" << getSeatNumber() << endl;
		cout << "Flight Number:" << getFlightNumber() << endl;
		cout << "************************" << endl;
		cout << endl;
	}

}