// FlightReservations.cpp : Defines the entry point for the console application.
#include "stdafx.h"
#include <iostream>
#include "Database.h"
#include <iomanip>
#include <array>
#include<cctype>

using namespace std;
using namespace FlightReservationApp;

// Global flight details.
array<string, 10> arrFlightNo = { "3476","169", "1703", "1598", "2369", "2259", "1705", "0589", "356945", "98758" };
array<string, 10> arrDepAirCode = { "MSP","PHX", "EWR", "MIA", "SEA", "CLT", "JFK", "SFO", "SEA", "ORD" };
array<string, 10> arrDeptime = { "0900","0630", "1200", "2130", "1750", "2350", "2215", "1315", "0945", "0330" };
array<string, 10> arrArrAirCode = { "MCO","LAS", "SEA", "JFK", "DEN", "DFW", "ORD", "LAX", "EWR", "CLT" };
array<string, 10> arrArrTime = { "1200","1100", "1600", "0030", "2200", "0530", "0200", "1730", "1230", "0515" };

int displayMenu();
void addBookingInfo(Database& db, string& seatNumber, string& flightNumber);
int availableSeat[10];
void initialize(char form[][6]);
void FlightDetails();

int main()
{
	// Menu Selection.
	Database flightReservationDB;
	Flight flight;

	while (true) {

		int selection = displayMenu();
		string flightNumberSelection;
		string seats[13];
		bool errorCheck = true;
		switch (selection)
		{
		case 1:
			FlightDetails();
			while (errorCheck) {
			cout << "Enter flight number you would like to book:";
			cin >> flightNumberSelection;
			if (find(begin(arrFlightNo), end(arrFlightNo), flightNumberSelection) != end(arrFlightNo))
			{
				cout << "You have selected the Flight Number: " << flightNumberSelection << endl;
				errorCheck = false;
			}
			else {
				cout << "The flight number selected was not on the list. Please select a new flight number." << endl;
				continue;
				}
			}
			char ticketType, column;
			int row;
			int noOfSeats;
			char form[13][6];
			cout << "How many Seats do you want to book for Flight:" << flightNumberSelection << "?";
			cin >> noOfSeats;
			for (int i = 0; i < noOfSeats; i++)
			{
				cout << "Select Seat:" << i+1 << endl;
				void initialize(char form[][6]);
				seats[i] = flight.GetData(ticketType, row, column);
				flight.SeatAvailabilityMap(form, flightNumberSelection, row, column);
				addBookingInfo(flightReservationDB, seats[i], flightNumberSelection);
			}
			cout << "Your Booking was successful" << endl;
			flightReservationDB.displayAllPassengers();
			break;	
		case 2: 
			cout << "Display Ticket Information" << endl;
			flightReservationDB.displayAllPassengers();
			break;
		case 3:
			cout << "Thank you for visiting." << endl;
			return 0;			
		default:
			cerr << "Unknown command." << endl;
			break;
		}
	}
    return 0;
}

int displayMenu()
{
	int selection;

	cout << endl;
	cout << "*********************" << endl;
	cout << "**Welcome to the Flight Reservation System!**" << endl;
	cout << "Please choose one of the menu options." << endl;
	cout << "-----------------" << endl;
	cout << "1) Book a Flight" << endl;
	cout << "2) Display Booked Ticket" << endl;
	cout << "3) Exit the flight booking." << endl;
	cout << "*********************" << endl;
	cout << endl;
	cout << "---> ";

	cout << "THE CHOSEN OPTION IS : ";
	cin >> selection;

	return selection;
}

void addBookingInfo(Database& db, string& seatNumber, string& flightNumber)
{
	string firstname;
	string lastName;
	string emailAddress;

	cout << "Please enter the passenger's first name: " << endl;
	cin >> firstname;

	cout << "Please enter the passenger's surname: " << endl;
	cin >> lastName;

	cout << "Please enter the passenger's email address for contact information: " << endl;
	cin >> emailAddress;

	db.addBooking(firstname, lastName, emailAddress, seatNumber, flightNumber);
}

void initialize(char form[][6])
{
	for (int i = 0; i < 13; i++)
		for (int j = 0; j<6; j++)
			form[i][j] = '*';
}

void FlightDetails()
{
	Flight flt;
	
	cout.setf(ios::left);
	cout << setw(15) << "Flight No." << setw(12) << "FROM" << setw(20) << "DEP. TIME" << setw(15) << "TO" << setw(30) << "ARR. TIME" << endl;
	cout << setw(15) << "----------" << setw(12) << "----" << setw(20) << "---------" << setw(15) << "--" << setw(30) << "---------" << endl;

	for (int i = 0; i < arrFlightNo.size() - 1; i++)
	{
		flt.setFlightNumber(arrFlightNo[i]);
		flt.setDepartureAirportCode(arrDepAirCode[i]);
		flt.setDepartureTime(arrDeptime[i]);
		flt.setArrivalAirportCode(arrArrAirCode[i]);
		flt.setArrivalTime(arrArrTime[i]);
		flt.display();
	}

}