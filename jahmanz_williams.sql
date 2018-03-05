-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Mar 05, 2018 at 03:24 AM
-- Server version: 5.6.34-log
-- PHP Version: 7.1.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `jahmanz_williams`
--
CREATE DATABASE IF NOT EXISTS `jahmanz_williams` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `jahmanz_williams`;

-- --------------------------------------------------------

--
-- Table structure for table `clients`
--

CREATE TABLE `clients` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `clients`
--

INSERT INTO `clients` (`id`, `Name`, `Email`) VALUES
(1, 'Laura Williams', 'lolosweets@gmail.com'),
(2, 'Florence Houston', 'flojoe@yahoo.com'),
(3, 'Ryan Green', 'papaboy@mac.com'),
(4, 'Gal Gadot', 'wonderwoman@gmail.com'),
(5, 'Ray Lewis', 'rayray@gmail.com'),
(6, 'Mildred Edordu', 'badgumbo@yahoo.com'),
(7, 'Jahmanz Williams', 'savojah@yahoo.com');

-- --------------------------------------------------------

--
-- Table structure for table `client_stylist`
--

CREATE TABLE `client_stylist` (
  `stylist_id` int(11) DEFAULT NULL,
  `client_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `client_stylist`
--

INSERT INTO `client_stylist` (`stylist_id`, `client_id`) VALUES
(1, 3),
(5, 1),
(2, 2),
(5, 4),
(2, 1),
(1, 3),
(3, 3),
(1, 4),
(1, 2),
(1, 1),
(5, 7),
(3, 5),
(NULL, 1),
(NULL, 1),
(NULL, 7),
(NULL, 7),
(NULL, 6);

-- --------------------------------------------------------

--
-- Table structure for table `specialty`
--

CREATE TABLE `specialty` (
  `id` int(11) NOT NULL,
  `specialty` varchar(255) NOT NULL,
  `stylist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `specialty`
--

INSERT INTO `specialty` (`id`, `specialty`, `stylist_id`) VALUES
(1, 'perm', 0),
(2, 'Mens Hairstyles', 0),
(3, 'highlights', 0),
(4, 'extensions', 0),
(5, 'Womens Hairstyles', 0);

-- --------------------------------------------------------

--
-- Table structure for table `stylists`
--

CREATE TABLE `stylists` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `stylists`
--

INSERT INTO `stylists` (`id`, `name`) VALUES
(1, 'Zohan '),
(2, 'Edward Scissorhands'),
(3, 'Florida Coleman'),
(5, 'Brutus \"The Barber\" Beefcake'),
(7, 'Rhonda');

-- --------------------------------------------------------

--
-- Table structure for table `stylist_specialty`
--

CREATE TABLE `stylist_specialty` (
  `specialty_id` int(11) DEFAULT NULL,
  `stylist_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `stylist_specialty`
--

INSERT INTO `stylist_specialty` (`specialty_id`, `stylist_id`) VALUES
(2, 4);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `specialty`
--
ALTER TABLE `specialty`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stylists`
--
ALTER TABLE `stylists`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clients`
--
ALTER TABLE `clients`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT for table `specialty`
--
ALTER TABLE `specialty`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT for table `stylists`
--
ALTER TABLE `stylists`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
