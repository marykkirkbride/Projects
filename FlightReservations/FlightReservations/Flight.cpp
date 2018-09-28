#include "stdafx.h"
#include <iostream>
#include "Flight.h"
#include "Passenger.h"
#include <iomanip>
#include "Seat.h"
#include<cctype>

using namespace std;
namespace FlightReservationApp {

	Flight::Flight(const std::string& flightNumber, 
		const std::string& departureAirportCode, 
		const std::string& departureTime, 
		const std::string& arrivalAirportCode, 
		const std::string& arrivalTime)

	{
		mFlightNumber = flightNumber;
		mDepartureAirportCode = departureAirportCode;
		mDepartureTime = departureTime;
		mArrivalAirportCode = arrivalAirportCode;
		mArrivalTime = arrivalTime;
	}

	Flight::Flight(const std::string& flightNumber)
	{
		mFlightNumber = flightNumber;
	}

	void Flight::SeatAvailabilityMap(char form[][6], const std::string& flightNumber, int& row, char& column)
	{
		
		try
		{
			cout << "Flight No - " << flightNumber << ". Seat Availability:" << endl;
			int i, j;

			if (form[row - 1][static_cast<int>(column - 65)] == 'X')
			{
				cout << "This seat already assigned. Choose another seat: " << endl;
				cin >> column;
				column = static_cast<char>(toupper(column));
			}
			form[row - 1][static_cast<int>(column) - 65] = 'X';

			cout << "* indicates that the seat is available; " << endl;
			cout << "X indicates that the seat is occupied. " << endl;
			cout << setw(12) << std::right << "A" << setw(6) << "B" << setw(6) << "C"
				<< setw(6) << "D" << setw(6) << "E" << setw(6) << "F" << endl;

			for (i = 0; i < 13; i++)
			{
				cout << left << setw(3) << "Row " << setw(2) << i + 1;
				for (j = 0; j < 6; j++)
				{
					cout << right << setw(6) << form[i][j];
				}
				cout << endl;
			}
		}
		catch (const std::exception&)
		{
			cout << "Exception Received";
		}

	}

	string Flight::GetData(char& ticketType, int& row, char& column)const
	{
		try
		{
			string seatNumber = "";
			cout << "The airplane has 13 rows, with  six seats in each row. " << endl;

			cout << "Enter ticket type,\n"
				<< "F for first class, \n"
				<< "B for business class,\n"
				<< "E for economy class:" << endl;
			cin >> ticketType;
			ticketType = static_cast<char>(toupper(ticketType));
			while (ticketType != 'F' && ticketType != 'B' && ticketType != 'E')
			{
				cout << "Invalid ticket type." << endl;
				cout << "Enter ticket type,\n"
					<< "F/f for first class, \n"
					<< "B/b for business class,\n"
					<< "E/e for economy class:" << endl;
				cin >> ticketType;
				ticketType = static_cast<char>(toupper(ticketType));
			}
			switch (ticketType)
			{
			case 'F':
				cout << "Row 1 and 2 are first class,\n";
				break;
			case 'B':
				cout << "row 3 throuh 7 are business class,\n";
				break;
			case 'E':
				cout << "row 8 through 13 are economy class." << endl;
				break;
			}// end switch
			cout << "Enter the row number you want to sit: " << endl;
			cin >> row;

			cout << "Enter the seat number (from A to F). " << endl;
			cin >> column;
			column = static_cast<char>(toupper(column));
			seatNumber = to_string(row) + string(1, column);
			return seatNumber;

		}
		catch (const std::exception&)
		{
			cout << "Exception Received";
		}
		
	}

	void Flight::display()const
	{
		cout.setf(ios::left);
		cout << setw(15) << getFlightNumber() << setw(12) << getDepartureAirportCode() << setw(20) << getDepartureTime() << setw(15) << getArrivalAirportCode() << setw(30) << getArrivalTime() << endl;
	}

	void Flight::setFlightNumber(const std::string& flightNumber)
	{
		mFlightNumber = flightNumber;
	}

	const std::string & Flight::getFlightNumber() const
	{

		return mFlightNumber;
	}

	void Flight::setDepartureAirportCode(const std::string& departureAirportCode)
	{
		mDepartureAirportCode = departureAirportCode;
	}

	const std::string & Flight::getDepartureAirportCode() const
	{
		return mDepartureAirportCode;
	}

	void Flight::setDepartureTime(const std::string & departureTime)
	{
		mDepartureTime = departureTime;
	}

	const std::string & Flight::getDepartureTime() const
	{
		return mDepartureTime;
	}

	void Flight::setArrivalAirportCode(const std::string & arrivalAirportCode)
	{
		mArrivalAirportCode = arrivalAirportCode;
	}

	const std::string & Flight::getArrivalAirportCode() const
	{
		return mArrivalAirportCode;
	}

	void Flight::setArrivalTime(const std::string & arrivalTime)
	{
		mArrivalTime = arrivalTime;
	}

	const std::string & Flight::getArrivalTime() const
	{
		return mArrivalTime;
	}
}

