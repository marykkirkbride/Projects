#pragma once
#include <string>
#include <vector>
#include "Seat.h"
#include <cctype>
#include "Passenger.h"

namespace FlightReservationApp {
	class Flight {

	public:
		Flight() = default;
		Flight(const std::string& flightNumber);
		Flight(const std::string& flightNumber,
			const std::string& departureAirportCode,
			const std::string& departureTime,
			const std::string& arrivalAirportCode,
			const std::string& arrivalTime);
		void display() const;

		//Getters and setters
		void setFlightNumber(const std::string& flightNumber);
		const std::string& getFlightNumber() const;

		void setDepartureAirportCode(const std::string& departureAirportCode);
		const std::string& getDepartureAirportCode() const;

		void setDepartureTime(const std::string& departureTime);
		const std::string& getDepartureTime() const;

		void setArrivalAirportCode(const std::string& arrivalAirportCode);
		const std::string& getArrivalAirportCode() const;

		void setArrivalTime(const std::string& arrivalTime);
		const std::string& getArrivalTime() const;

		void SeatAvailabilityMap(char form[][6], const std::string& flightNumber, int& row, char& column) ;
		std::string GetData(char& ticketType, int& row, char& column)const;

	private:
		std::string mFlightNumber;
		std::string mDepartureAirportCode;
		std::string mDepartureTime;
		std::string mArrivalAirportCode;
		std::string mArrivalTime;
		std::vector<Seat> mSeats;
	};
}