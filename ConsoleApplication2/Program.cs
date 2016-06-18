﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FlightModel> flightsList = GetFlightList();

            flightsList.OrderBy(flight => flight.ScheduledDateTime);

            var filteredFlightList = flightsList.Where(f => f.CanFollow == true);

            ConsoleTableWriter("AorD", "CanFollow", "Destination", "FlightNumber", "ScheduledDateTime", "Status", "Terminal");

            var currentTime = Convert.ToDateTime("2016-06-17T21:00:00");



            foreach (var flight in filteredFlightList)
            {
                ConsoleTableWriter(flight.AorD.ToString(), flight.CanFollow.ToString(), flight.Destination, flight.FlightNumber, flight.ScheduledDateTime.ToString(), flight.Status, flight.Terminal);
            }

            //| 0 | True | Glasgow | FR7493 | 17 / 06 / 2016 22:15:00 | Scheduled | T1 |
            //| 0 | True | Plovdiv | FR1837 | 17 / 06 / 2016 22:25:00 | Expected 22:17 | T1 |
            //| 0 | True | Bilbao | EZY3228 | 17 / 06 / 2016 22:30:00 | Scheduled | T1 |
            //| 0 | True | Belfast | EZY262 | 17 / 06 / 2016 22:35:00 | Estimated 23:10 | T1 |
            //| 0 | True | Milan Malpensa | FR8737 | 17 / 06 / 2016 22:40:00 | Scheduled | T1 |
            //| 0 | True | Riga | FR2645 | 17 / 06 / 2016 22:40:00 | Expected 23:40 | T1 |
            //| 0 | False | Naples | EZY3252 | 17 / 06 / 2016 22:50:00 | Cancelled | T1 |
            //| 0 | True | Shannon | FR108 | 17 / 06 / 2016 22:50:00 | Estimated 23:45 | T1 |
            //| 0 | True | Istanbul | PC519 | 17 / 06 / 2016 22:55:00 | Expected 22:56 | T1 |
            //| 0 | True | Frankfurt Hahn | FR761 | 17 / 06 / 2016 22:55:00 | Scheduled | T1 |
            //| 0 | True | Murcia | FR8027 | 17 / 06 / 2016 22:55:00 | Expected 23:15 | T1 |
            //| 0 | True | Warsaw Modlin | FR2284 | 17 / 06 / 2016 23:10:00 | Scheduled | T1 |
            //| 0 | True | Billund | FR5179 | 17 / 06 / 2016 23:10:00 | Scheduled | T1 |

            Console.ReadKey();
        }

        private static List<FlightModel> GetFlightList()
        {
            var arrivalsRawData = new WebClient().DownloadString("http://www.stanstedairport.com/umbraco/api/flightinformationapi/getArrivals?airportCode=STN&t1=true&t2=false&t3=false");
            //string arrivalsRawData = "{\"Flights\":[{\"FlightNumber\":\"FR3132\",\"AorD\":0,\"Destination\":\"Paphos\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T16:35:00\",\"Status\":\"Estimated 04:25\",\"CanFollow\":true},{\"FlightNumber\":\"FR795\",\"AorD\":0,\"Destination\":\"Venice Treviso\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T17:45:00\",\"Status\":\"Landed 19:53\",\"CanFollow\":false},{\"FlightNumber\":\"FR146\",\"AorD\":0,\"Destination\":\"Berlin Schonefe\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T18:55:00\",\"Status\":\"Landed 19:21\",\"CanFollow\":false},{\"FlightNumber\":\"FR7497\",\"AorD\":0,\"Destination\":\"Glasgow\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:00:00\",\"Status\":\"Landed 19:58\",\"CanFollow\":false},{\"FlightNumber\":\"FR2817\",\"AorD\":0,\"Destination\":\"Cologne\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:05:00\",\"Status\":\"Landed 20:06\",\"CanFollow\":false},{\"FlightNumber\":\"EZY240\",\"AorD\":0,\"Destination\":\"Edinburgh\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:10:00\",\"Status\":\"Landed 20:11\",\"CanFollow\":false},{\"FlightNumber\":\"FR8387\",\"AorD\":0,\"Destination\":\"Palma Mallorca\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:10:00\",\"Status\":\"Landed 19:16\",\"CanFollow\":false},{\"FlightNumber\":\"FR3003\",\"AorD\":0,\"Destination\":\"Rome Ciampino\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:15:00\",\"Status\":\"Landed 19:50\",\"CanFollow\":false},{\"FlightNumber\":\"EZY3006\",\"AorD\":0,\"Destination\":\"Amsterdam\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:20:00\",\"Status\":\"Landed 20:37\",\"CanFollow\":false},{\"FlightNumber\":\"FR589\",\"AorD\":0,\"Destination\":\"Pisa\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:25:00\",\"Status\":\"Landed 19:13\",\"CanFollow\":false},{\"FlightNumber\":\"FR2319\",\"AorD\":0,\"Destination\":\"Bratislava\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:25:00\",\"Status\":\"Landed 19:11\",\"CanFollow\":false},{\"FlightNumber\":\"FR1395\",\"AorD\":0,\"Destination\":\"Oslo Rygge\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:30:00\",\"Status\":\"Landed 19:46\",\"CanFollow\":false},{\"FlightNumber\":\"FR5997\",\"AorD\":0,\"Destination\":\"Madrid\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:30:00\",\"Status\":\"Landed 19:35\",\"CanFollow\":false},{\"FlightNumber\":\"FR8888\",\"AorD\":0,\"Destination\":\"Edinburgh\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:35:00\",\"Status\":\"Landed 19:38\",\"CanFollow\":false},{\"FlightNumber\":\"FR4197\",\"AorD\":0,\"Destination\":\"Milan Bergamo\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:40:00\",\"Status\":\"Landed 19:43\",\"CanFollow\":false},{\"FlightNumber\":\"4U2378\",\"AorD\":0,\"Destination\":\"Stuttgart\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:45:00\",\"Status\":\"Landed 20:50\",\"CanFollow\":false},{\"FlightNumber\":\"4U3370\",\"AorD\":0,\"Destination\":\"Hanover\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:45:00\",\"Status\":\"Landed 19:40\",\"CanFollow\":false},{\"FlightNumber\":\"FR1195\",\"AorD\":0,\"Destination\":\"Bologna\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:45:00\",\"Status\":\"Landed 19:49\",\"CanFollow\":false},{\"FlightNumber\":\"FR2670\",\"AorD\":0,\"Destination\":\"Warsaw Modlin\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:45:00\",\"Status\":\"Landed 20:08\",\"CanFollow\":false},{\"FlightNumber\":\"FR104\",\"AorD\":0,\"Destination\":\"Shannon\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:50:00\",\"Status\":\"Landed 20:14\",\"CanFollow\":false},{\"FlightNumber\":\"4U356\",\"AorD\":0,\"Destination\":\"Cologne\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T19:55:00\",\"Status\":\"Estimated 21:50\",\"CanFollow\":true},{\"FlightNumber\":\"FR906\",\"AorD\":0,\"Destination\":\"Cork\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T20:10:00\",\"Status\":\"Landed 20:27\",\"CanFollow\":false},{\"FlightNumber\":\"FR9815\",\"AorD\":0,\"Destination\":\"Barcelona\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T20:10:00\",\"Status\":\"Landed 20:22\",\"CanFollow\":false},{\"FlightNumber\":\"EZY214\",\"AorD\":0,\"Destination\":\"Glasgow\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T20:15:00\",\"Status\":\"Landed 20:25\",\"CanFollow\":false},{\"FlightNumber\":\"FR288\",\"AorD\":0,\"Destination\":\"Dublin\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T20:15:00\",\"Status\":\"Landed 20:45\",\"CanFollow\":false},{\"FlightNumber\":\"FR8408\",\"AorD\":0,\"Destination\":\"Wroclaw\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T20:15:00\",\"Status\":\"Landed 19:55\",\"CanFollow\":false},{\"FlightNumber\":\"FR8348\",\"AorD\":0,\"Destination\":\"Porto\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T20:30:00\",\"Status\":\"Landed 20:52\",\"CanFollow\":false},{\"FlightNumber\":\"EZY242\",\"AorD\":0,\"Destination\":\"Edinburgh\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T20:55:00\",\"Status\":\"Landed 21:06\",\"CanFollow\":false},{\"FlightNumber\":\"EZY260\",\"AorD\":0,\"Destination\":\"Belfast\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T20:55:00\",\"Status\":\"Landed 20:54\",\"CanFollow\":false},{\"FlightNumber\":\"FR8824\",\"AorD\":0,\"Destination\":\"Edinburgh\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T21:00:00\",\"Status\":\"Landed 20:42\",\"CanFollow\":false},{\"FlightNumber\":\"FR9274\",\"AorD\":0,\"Destination\":\"Eindhoven\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T21:05:00\",\"Status\":\"Expected 21:15\",\"CanFollow\":true},{\"FlightNumber\":\"EZY3010\",\"AorD\":0,\"Destination\":\"Amsterdam\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T21:30:00\",\"Status\":\"Estimated 21:55\",\"CanFollow\":true},{\"FlightNumber\":\"FR272\",\"AorD\":0,\"Destination\":\"Dublin\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T21:40:00\",\"Status\":\"Expected 22:06\",\"CanFollow\":true},{\"FlightNumber\":\"FR3633\",\"AorD\":0,\"Destination\":\"Bremen\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T21:45:00\",\"Status\":\"Expected 21:55\",\"CanFollow\":true},{\"FlightNumber\":\"MT7129\",\"AorD\":0,\"Destination\":\"Tenerife\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T21:55:00\",\"Status\":\"Expected 21:57\",\"CanFollow\":true},{\"FlightNumber\":\"FR2815\",\"AorD\":0,\"Destination\":\"Cologne\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR7493\",\"AorD\":0,\"Destination\":\"Glasgow\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:15:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR1837\",\"AorD\":0,\"Destination\":\"Plovdiv\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:25:00\",\"Status\":\"Expected 22:17\",\"CanFollow\":true},{\"FlightNumber\":\"EZY3228\",\"AorD\":0,\"Destination\":\"Bilbao\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:30:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"EZY262\",\"AorD\":0,\"Destination\":\"Belfast\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:35:00\",\"Status\":\"Estimated 23:10\",\"CanFollow\":true},{\"FlightNumber\":\"FR8737\",\"AorD\":0,\"Destination\":\"Milan Malpensa\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:40:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR2645\",\"AorD\":0,\"Destination\":\"Riga\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:40:00\",\"Status\":\"Expected 23:40\",\"CanFollow\":true},{\"FlightNumber\":\"EZY3252\",\"AorD\":0,\"Destination\":\"Naples\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:50:00\",\"Status\":\"Cancelled\",\"CanFollow\":false},{\"FlightNumber\":\"FR108\",\"AorD\":0,\"Destination\":\"Shannon\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:50:00\",\"Status\":\"Estimated 23:45\",\"CanFollow\":true},{\"FlightNumber\":\"PC519\",\"AorD\":0,\"Destination\":\"Istanbul\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:55:00\",\"Status\":\"Expected 22:56\",\"CanFollow\":true},{\"FlightNumber\":\"FR761\",\"AorD\":0,\"Destination\":\"Frankfurt Hahn\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:55:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8027\",\"AorD\":0,\"Destination\":\"Murcia\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T22:55:00\",\"Status\":\"Expected 23:15\",\"CanFollow\":true},{\"FlightNumber\":\"FR2284\",\"AorD\":0,\"Destination\":\"Warsaw Modlin\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR5179\",\"AorD\":0,\"Destination\":\"Billund\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"EZY3116\",\"AorD\":0,\"Destination\":\"Malaga\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR2467\",\"AorD\":0,\"Destination\":\"Szczecin\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR9968\",\"AorD\":0,\"Destination\":\"Sofia\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:10:00\",\"Status\":\"Expected 23:17\",\"CanFollow\":true},{\"FlightNumber\":\"FR2613\",\"AorD\":0,\"Destination\":\"Santander\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR298\",\"AorD\":0,\"Destination\":\"Dublin\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:15:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR9252\",\"AorD\":0,\"Destination\":\"Ibiza\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:20:00\",\"Status\":\"Expected 00:14\",\"CanFollow\":true},{\"FlightNumber\":\"FR059\",\"AorD\":0,\"Destination\":\"Stockholm Skavs\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:25:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR2437\",\"AorD\":0,\"Destination\":\"Krakow\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:25:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8545\",\"AorD\":0,\"Destination\":\"Berlin Schonefe\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:25:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8369\",\"AorD\":0,\"Destination\":\"Budapest\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:25:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8157\",\"AorD\":0,\"Destination\":\"Tenerife\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:30:00\",\"Status\":\"Expected 23:47\",\"CanFollow\":true},{\"FlightNumber\":\"EZY3216\",\"AorD\":0,\"Destination\":\"Palma Mallorca\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:30:00\",\"Status\":\"Estimated 00:59\",\"CanFollow\":true},{\"FlightNumber\":\"FR2245\",\"AorD\":0,\"Destination\":\"Vilnius\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:35:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR2137\",\"AorD\":0,\"Destination\":\"Rzeszow\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:35:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR799\",\"AorD\":0,\"Destination\":\"Venice Treviso\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:40:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR1906\",\"AorD\":0,\"Destination\":\"Bari\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:40:00\",\"Status\":\"Estimated 00:30\",\"CanFollow\":true},{\"FlightNumber\":\"FR2373\",\"AorD\":0,\"Destination\":\"Gdansk\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:40:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR1883\",\"AorD\":0,\"Destination\":\"Lisbon\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:45:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR3074\",\"AorD\":0,\"Destination\":\"Comiso\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:45:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR3919\",\"AorD\":0,\"Destination\":\"Palermo\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:45:00\",\"Status\":\"Estimated 00:59\",\"CanFollow\":true},{\"FlightNumber\":\"FR2006\",\"AorD\":0,\"Destination\":\"Bucharest\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:50:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR2014\",\"AorD\":0,\"Destination\":\"Prague\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-17T23:55:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8583\",\"AorD\":0,\"Destination\":\"Thessaloniki\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T00:00:00\",\"Status\":\"Estimated 01:10\",\"CanFollow\":true},{\"FlightNumber\":\"FR6544\",\"AorD\":0,\"Destination\":\"Marseille\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T00:00:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8165\",\"AorD\":0,\"Destination\":\"Malaga\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T00:00:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"TOM5553\",\"AorD\":0,\"Destination\":\"Tenerife\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T00:35:00\",\"Status\":\"Expected 00:45\",\"CanFollow\":true},{\"FlightNumber\":\"TOM5557\",\"AorD\":0,\"Destination\":\"Corfu\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T02:00:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"MT7509\",\"AorD\":0,\"Destination\":\"Dalaman\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T02:55:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"EZY3048\",\"AorD\":0,\"Destination\":\"Ibiza\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T03:30:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR753\",\"AorD\":0,\"Destination\":\"Frankfurt Hahn\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T06:50:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR3253\",\"AorD\":0,\"Destination\":\"Dusseldorf Weez\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:05:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR2813\",\"AorD\":0,\"Destination\":\"Cologne\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR3631\",\"AorD\":0,\"Destination\":\"Bremen\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:15:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR144\",\"AorD\":0,\"Destination\":\"Berlin Schonefe\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:25:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR4191\",\"AorD\":0,\"Destination\":\"Milan Bergamo\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:40:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"EW5832\",\"AorD\":0,\"Destination\":\"Vienna\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:45:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR5993\",\"AorD\":0,\"Destination\":\"Madrid\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:45:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR202\",\"AorD\":0,\"Destination\":\"Dublin\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:50:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR9811\",\"AorD\":0,\"Destination\":\"Barcelona\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:50:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR2315\",\"AorD\":0,\"Destination\":\"Bratislava\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:50:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR053\",\"AorD\":0,\"Destination\":\"Stockholm Skavs\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:55:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR902\",\"AorD\":0,\"Destination\":\"Cork\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:55:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8728\",\"AorD\":0,\"Destination\":\"Milan Malpensa\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T07:55:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8354\",\"AorD\":0,\"Destination\":\"Budapest\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:05:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR585\",\"AorD\":0,\"Destination\":\"Pisa\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:05:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR195\",\"AorD\":0,\"Destination\":\"Bologna\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:05:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR1393\",\"AorD\":0,\"Destination\":\"Oslo Rygge\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8406\",\"AorD\":0,\"Destination\":\"Wroclaw\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR9272\",\"AorD\":0,\"Destination\":\"Eindhoven\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:10:00\",\"Status\":\"Estimated 11:10\",\"CanFollow\":true},{\"FlightNumber\":\"FR9803\",\"AorD\":0,\"Destination\":\"Girona Barcelon\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:10:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR7491\",\"AorD\":0,\"Destination\":\"Glasgow\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:15:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR3005\",\"AorD\":0,\"Destination\":\"Rome Ciampino\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:15:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR1022\",\"AorD\":0,\"Destination\":\"Warsaw Modlin\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:15:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR965\",\"AorD\":0,\"Destination\":\"Gothenburg Lr\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:20:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR2433\",\"AorD\":0,\"Destination\":\"Krakow\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:20:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8882\",\"AorD\":0,\"Destination\":\"Edinburgh\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:25:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR014\",\"AorD\":0,\"Destination\":\"Athens\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:30:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR102\",\"AorD\":0,\"Destination\":\"Shannon\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T08:35:00\",\"Status\":\"Scheduled\",\"CanFollow\":true},{\"FlightNumber\":\"FR8344\",\"AorD\":0,\"Destination\":\"Porto\",\"Terminal\":\"T1\",\"ScheduledDateTime\":\"2016-06-18T09:00:00\",\"Status\":\"Scheduled\",\"CanFollow\":true}]}";

            var result = JsonConvert.DeserializeObject<FlightsModel>(arrivalsRawData).Flights;

            List<FlightModel> flightsList = result.ToList<FlightModel>();
            return flightsList;
        }

        static void ConsoleTableWriter (string arg0, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            var line = String.Format("|{0,4}|{1,9}|{2,16}|{3,12}|{4,19}|{5,16}|{6,8}|", arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            Console.WriteLine(line);
        }
    }
}
