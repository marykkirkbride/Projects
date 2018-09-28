#include "stdafx.h"
#include "Flight.h"
#include "Seat.h"
#include <iostream>
#include  <string>
using namespace std;

// The Total number of seats on the flight
int totalSeats = 10;


namespace FlightReservationApp {
	// Seat::Seat(){} // Do I need this constructor?

	bool Seat::isBooked() const
	{
		return mBooked;
	}

	void Seat::setSeatNumber(int seatNumber) {
		mseatNumber = seatNumber;
	}

	int Seat::getSeatNumber() const {
		return mseatNumber;
	}



	int currentTotalSeats(int seatsBooked)
	{
		totalSeats = totalSeats - seatsBooked;

		return totalSeats;
	}

}