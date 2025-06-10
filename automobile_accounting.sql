USE [master]
GO

/****** Object:  Database [automobile_accounting]    Script Date: 11.06.2025 2:09:44 ******/
CREATE DATABASE [automobile_accounting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'automobile_accounting', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\automobile_accounting.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'automobile_accounting_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\automobile_accounting_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [automobile_accounting].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [automobile_accounting] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [automobile_accounting] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [automobile_accounting] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [automobile_accounting] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [automobile_accounting] SET ARITHABORT OFF 
GO

ALTER DATABASE [automobile_accounting] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [automobile_accounting] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [automobile_accounting] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [automobile_accounting] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [automobile_accounting] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [automobile_accounting] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [automobile_accounting] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [automobile_accounting] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [automobile_accounting] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [automobile_accounting] SET  DISABLE_BROKER 
GO

ALTER DATABASE [automobile_accounting] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [automobile_accounting] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [automobile_accounting] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [automobile_accounting] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [automobile_accounting] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [automobile_accounting] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [automobile_accounting] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [automobile_accounting] SET RECOVERY FULL 
GO

ALTER DATABASE [automobile_accounting] SET  MULTI_USER 
GO

ALTER DATABASE [automobile_accounting] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [automobile_accounting] SET DB_CHAINING OFF 
GO

ALTER DATABASE [automobile_accounting] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [automobile_accounting] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [automobile_accounting] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [automobile_accounting] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [automobile_accounting] SET QUERY_STORE = ON
GO

ALTER DATABASE [automobile_accounting] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [automobile_accounting] SET  READ_WRITE 
GO

