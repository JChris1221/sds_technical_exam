USE [master]
GO
/****** Object:  Database [sds_technical_exam]    Script Date: 6/17/2023 2:50:29 PM ******/
CREATE DATABASE [sds_technical_exam]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sds_technical_exam', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\sds_technical_exam.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'sds_technical_exam_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\sds_technical_exam_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [sds_technical_exam] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sds_technical_exam].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sds_technical_exam] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sds_technical_exam] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sds_technical_exam] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sds_technical_exam] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sds_technical_exam] SET ARITHABORT OFF 
GO
ALTER DATABASE [sds_technical_exam] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [sds_technical_exam] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sds_technical_exam] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sds_technical_exam] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sds_technical_exam] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sds_technical_exam] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sds_technical_exam] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sds_technical_exam] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sds_technical_exam] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sds_technical_exam] SET  ENABLE_BROKER 
GO
ALTER DATABASE [sds_technical_exam] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sds_technical_exam] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sds_technical_exam] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sds_technical_exam] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sds_technical_exam] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sds_technical_exam] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sds_technical_exam] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sds_technical_exam] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [sds_technical_exam] SET  MULTI_USER 
GO
ALTER DATABASE [sds_technical_exam] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sds_technical_exam] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sds_technical_exam] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sds_technical_exam] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [sds_technical_exam] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [sds_technical_exam] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [sds_technical_exam] SET QUERY_STORE = ON
GO
ALTER DATABASE [sds_technical_exam] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [sds_technical_exam]
GO
/****** Object:  Table [dbo].[Recyclable_Item]    Script Date: 6/17/2023 2:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recyclable_Item](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecyclableTypeId] [int] NOT NULL,
	[Weight] [decimal](38, 2) NOT NULL,
	[ComputedRate] [decimal](38, 2) NOT NULL,
	[ItemDescription] [varchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recyclable_Type]    Script Date: 6/17/2023 2:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recyclable_Type](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[Rate] [decimal](38, 2) NOT NULL,
	[MinKG] [decimal](38, 2) NOT NULL,
	[MaxKG] [decimal](38, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Recyclable_Item]  WITH CHECK ADD FOREIGN KEY([RecyclableTypeId])
REFERENCES [dbo].[Recyclable_Type] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[spInsert_Recyclable_Item]    Script Date: 6/17/2023 2:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROC [dbo].[spInsert_Recyclable_Item](@recyclable_type_id AS INT, @weight AS DECIMAL(38,2), @item_description AS VARCHAR(150))
AS
BEGIN
	DECLARE @computed_rate DECIMAL(38,2)
	DECLARE @rate DECIMAL(38,2)
	SELECT @rate = Rate FROM Recyclable_Type WHERE Id = @recyclable_type_id
	SET @computed_rate = CAST((@rate*@weight)as DECIMAL(38,2))
	
	INSERT INTO [Recyclable_Item]([RecyclableTypeId], [Weight],[ComputedRate], [ItemDescription])
	VALUES (@recyclable_type_id, @weight, @computed_rate,@item_description)
END
                    
GO
/****** Object:  StoredProcedure [dbo].[spUpdate_Recyclable_Item]    Script Date: 6/17/2023 2:50:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[spUpdate_Recyclable_Item](@id as INT, @recyclable_type_id AS INT, @weight AS DECIMAL(38,2), @item_description AS VARCHAR(150))
AS
BEGIN
	DECLARE @computed_rate DECIMAL(38,2)
	DECLARE @rate DECIMAL(38,2)
	SELECT @rate = Rate FROM Recyclable_Type WHERE Id = @recyclable_type_id
	SET @computed_rate = CAST ((@rate*@weight)as DECIMAL(38,2))

	UPDATE [Recyclable_Item] SET
	[RecyclableTypeId] = @recyclable_type_id,
	[Weight] = @weight,
	[ComputedRate] = @computed_rate,
	[ItemDescription] = @item_description
	WHERE Id = @id
END
GO
USE [master]
GO
ALTER DATABASE [sds_technical_exam] SET  READ_WRITE 
GO
