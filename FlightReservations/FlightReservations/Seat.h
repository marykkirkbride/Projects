#pragma once

namespace FlightReservationApp {

	class Seat
	{
	public:
		Seat() = default; //Do I need to set default constructor?
		~Seat() = default;

		void setSeatNumber(int SeatNumber);
		int getSeatNumber() const;

		bool isBooked() const;
	private:
		int mseatNumber = -1; // Do I need to initialize this here?
		bool mBooked = false;
	};
}