USE [master]
GO
/****** Object:  Database [automobile_accounting]    Script Date: 11.06.2025 2:18:11 ******/
CREATE DATABASE [automobile_accounting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'automobile_accounting', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\automobile_accounting.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'automobile_accounting_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\automobile_accounting_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [automobile_accounting] SET COMPATIBILITY_LEVEL = 160
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
EXEC sys.sp_db_vardecimal_storage_format N'automobile_accounting', N'ON'
GO
ALTER DATABASE [automobile_accounting] SET QUERY_STORE = ON
GO
ALTER DATABASE [automobile_accounting] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [automobile_accounting]
GO
/****** Object:  Table [dbo].[avto]    Script Date: 11.06.2025 2:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[avto](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[id_avto] [int] NOT NULL,
	[serial_number] [nchar](10) NOT NULL,
	[brend] [nvarchar](50) NOT NULL,
	[model] [nvarchar](50) NOT NULL,
	[configuration] [nvarchar](50) NOT NULL,
	[year] [int] NOT NULL,
	[kuzov] [nvarchar](50) NOT NULL,
	[color] [nvarchar](50) NOT NULL,
	[price] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_avto] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 11.06.2025 2:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[num_order] [int] NOT NULL,
	[date_order] [date] NOT NULL,
	[name_brend] [nvarchar](50) NOT NULL,
	[name_model] [nvarchar](50) NOT NULL,
	[name_configuration] [nvarchar](50) NOT NULL,
	[name_color] [nvarchar](50) NOT NULL,
	[quantity_avto] [nvarchar](50) NOT NULL,
	[sum_sale] [decimal](18, 0) NOT NULL,
	[name_partner] [nvarchar](50) NOT NULL,
	[fio_partner] [nvarchar](50) NOT NULL,
	[discount] [decimal](18, 0) NOT NULL,
	[sum_sale_discount] [decimal](18, 0) NULL,
	[contract] [bit] NULL,
	[ID_avto] [int] NULL,
	[ID_partners] [int] NULL,
	[ID_reports] [int] NULL,
	[ID_role] [int] NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[partners]    Script Date: 11.06.2025 2:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partners](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[id_partner] [int] NOT NULL,
	[name_partner] [nvarchar](50) NOT NULL,
	[fio_partner] [nvarchar](max) NOT NULL,
	[email_partner] [nvarchar](50) NOT NULL,
	[phone_partenr] [nvarchar](50) NULL,
 CONSTRAINT [PK_partners] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[report]    Script Date: 11.06.2025 2:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[report](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Id_report] [int] NOT NULL,
	[date_report] [date] NOT NULL,
	[date_start] [date] NOT NULL,
	[date_end] [date] NOT NULL,
	[quantity_avto_sale] [decimal](18, 0) NOT NULL,
	[quantity_sum_sale] [decimal](18, 0) NOT NULL,
	[quantity_order] [int] NOT NULL,
 CONSTRAINT [PK_report] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 11.06.2025 2:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[id_role] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[fio] [nvarchar](max) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_avto] FOREIGN KEY([ID_avto])
REFERENCES [dbo].[avto] ([ID])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_avto]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_partners] FOREIGN KEY([ID_partners])
REFERENCES [dbo].[partners] ([ID])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_partners]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_report] FOREIGN KEY([ID_reports])
REFERENCES [dbo].[report] ([ID])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_report]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_role] FOREIGN KEY([ID_role])
REFERENCES [dbo].[role] ([ID])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_role]
GO
USE [master]
GO
ALTER DATABASE [automobile_accounting] SET  READ_WRITE 
GO
