USE [master]
GO
/****** Object:  Database [PayPal Integrate]    Script Date: 12/16/2020 8:13:54 AM ******/
CREATE DATABASE [PayPal Integrate]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PayPal Integrate', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\PayPal Integrate.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PayPal Integrate_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\PayPal Integrate_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PayPal Integrate] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PayPal Integrate].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PayPal Integrate] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PayPal Integrate] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PayPal Integrate] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PayPal Integrate] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PayPal Integrate] SET ARITHABORT OFF 
GO
ALTER DATABASE [PayPal Integrate] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PayPal Integrate] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PayPal Integrate] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PayPal Integrate] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PayPal Integrate] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PayPal Integrate] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PayPal Integrate] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PayPal Integrate] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PayPal Integrate] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PayPal Integrate] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PayPal Integrate] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PayPal Integrate] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PayPal Integrate] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PayPal Integrate] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PayPal Integrate] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PayPal Integrate] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PayPal Integrate] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PayPal Integrate] SET RECOVERY FULL 
GO
ALTER DATABASE [PayPal Integrate] SET  MULTI_USER 
GO
ALTER DATABASE [PayPal Integrate] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PayPal Integrate] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PayPal Integrate] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PayPal Integrate] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PayPal Integrate] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PayPal Integrate', N'ON'
GO
ALTER DATABASE [PayPal Integrate] SET QUERY_STORE = OFF
GO
USE [PayPal Integrate]
GO
/****** Object:  Table [dbo].[tbl_customerInvoice]    Script Date: 12/16/2020 8:13:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_customerInvoice](
	[idtbl_customerInvoice] [bigint] IDENTITY(1,1) NOT NULL,
	[customerFname] [nvarchar](100) NOT NULL,
	[customerEmail] [nvarchar](100) NOT NULL,
	[amount] [float] NOT NULL,
	[System Date] [datetime] NULL,
	[customerLname] [nvarchar](100) NULL,
	[invoicerLname] [nvarchar](100) NULL,
	[invoicerFname] [nvarchar](100) NULL,
	[invoicerStreet] [nvarchar](100) NULL,
	[invoicerTown] [nvarchar](100) NULL,
	[invoicerState] [nvarchar](100) NULL,
	[invoicerPostCode] [nvarchar](100) NULL,
	[invoicerCountry] [nvarchar](100) NULL,
	[invoicerCountryCode] [nvarchar](100) NULL,
	[invoicerPhone] [nvarchar](100) NULL,
	[invoicerWebsite] [nvarchar](100) NULL,
	[invoicerAdditionalInfo] [nvarchar](100) NULL,
	[invoicerLogoUrl] [nvarchar](100) NULL,
	[invoicerTaxId] [nvarchar](100) NULL,
	[invoicerRefNum] [nvarchar](100) NULL,
	[customerCountryCode] [nvarchar](100) NULL,
	[customerPhone] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idtbl_customerInvoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [PayPal Integrate] SET  READ_WRITE 
GO
