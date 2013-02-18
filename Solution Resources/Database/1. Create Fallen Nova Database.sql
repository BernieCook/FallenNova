/*
These scripts create the Fallen Nova database in SQL 2008 R2

NOTE:
Feel free to alter the database name and default .mdf/.ldf location. 
If you change the database name please update the web.config in the MVC web application accordingly.

*/

USE [master]
GO
/****** Object:  Database [FallenNovaDevelopment]    Script Date: 01/13/2013 09:00:03 ******/
CREATE DATABASE [FallenNovaDevelopment] ON  PRIMARY 
( NAME = N'FallenNovaDevelopment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQL2008\MSSQL\DATA\FallenNovaDevelopment.mdf' , SIZE = 37888KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FallenNovaDevelopment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQL2008\MSSQL\DATA\FallenNovaDevelopment_1.ldf' , SIZE = 265344KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FallenNovaDevelopment] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FallenNovaDevelopment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FallenNovaDevelopment] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET ANSI_NULLS OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET ANSI_PADDING OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET ARITHABORT OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [FallenNovaDevelopment] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [FallenNovaDevelopment] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [FallenNovaDevelopment] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET  DISABLE_BROKER
GO
ALTER DATABASE [FallenNovaDevelopment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [FallenNovaDevelopment] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [FallenNovaDevelopment] SET  READ_WRITE
GO
ALTER DATABASE [FallenNovaDevelopment] SET RECOVERY FULL
GO
ALTER DATABASE [FallenNovaDevelopment] SET  MULTI_USER
GO
ALTER DATABASE [FallenNovaDevelopment] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [FallenNovaDevelopment] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'FallenNovaDevelopment', N'ON'
GO
USE [FallenNovaDevelopment]
GO
/****** Object:  Table [dbo].[User]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserStatusId] [int] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[Surname] [nvarchar](100) NOT NULL,
	[EmailAddress] [nvarchar](200) NULL,
	[HashedPassword] [nvarchar](200) NULL,
	[UnsuccessfulLoginAttempts] [int] NOT NULL,
	[LastSuccessfulAuthenticationDateTime] [datetime] NULL,
	[LastSuccessfulLoginDateTime] [datetime] NULL,
	[AddedByUserId] [int] NOT NULL,
	[AddedDateTime] [datetime] NOT NULL,
	[ModifiedByUserId] [int] NOT NULL,
	[ModifiedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [ID_EmailAddress] ON [dbo].[User] 
(
	[EmailAddress] ASC
)
WHERE ([EmailAddress] IS NOT NULL)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[File]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](200) NOT NULL,
	[ContentType] [varchar](50) NULL,
	[FileSizeInBytes] [bigint] NULL,
	[BlobData] [varbinary](max) NOT NULL,
	[DownloadCount] [bigint] NOT NULL,
	[LastDownloadedDateTime] [datetime] NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserLogType]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLogType](
	[UserLogTypeId] [int] NOT NULL,
	[Title] [varchar](200) NOT NULL,
 CONSTRAINT [PK_UserLogType] PRIMARY KEY CLUSTERED 
(
	[UserLogTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserStatus](
	[UserStatusId] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
 CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED 
(
	[UserStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EveOnlineIcon](
	[EveOnlineIconId] [int] NOT NULL,
	[FileNameWithoutExtension] [varchar](500) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EveOnlineIcon] PRIMARY KEY CLUSTERED 
(
	[EveOnlineIconId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Corporation]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Corporation](
	[CorporationId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Corporation] PRIMARY KEY CLUSTERED 
(
	[CorporationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineSkillGroup]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineSkillGroup](
	[EveOnlineSkillGroupId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_EveOnlineSkillGroup] PRIMARY KEY CLUSTERED 
(
	[EveOnlineSkillGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineRace]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineRace](
	[EveOnlineRaceId] [int] NOT NULL,
	[EveOnlineIconId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ShortDescription] [nvarchar](500) NULL,
 CONSTRAINT [PK_EveOnlineRace] PRIMARY KEY CLUSTERED 
(
	[EveOnlineRaceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineMarketGroup]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineMarketGroup](
	[EveOnlineMarketGroupId] [int] NOT NULL,
	[ParentEveOnlineMarketGroupId] [int] NULL,
	[EveOnlineIconId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[HasTypes] [bit] NULL,
 CONSTRAINT [PK_EveOnlineMarketGroup] PRIMARY KEY CLUSTERED 
(
	[EveOnlineMarketGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineFaction]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineFaction](
	[EveOnlineFactionId] [int] NOT NULL,
	[EveOnlineIconId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EveOnlineFaction] PRIMARY KEY CLUSTERED 
(
	[EveOnlineFactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineCategory]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineCategory](
	[EveOnlineCategoryId] [int] NOT NULL,
	[EveOnlineIconId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EveOnlineCategory] PRIMARY KEY CLUSTERED 
(
	[EveOnlineCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineAttribute]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineAttribute](
	[EveOnlineAttributeId] [int] NOT NULL,
	[EveOnlineIconId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ShortDescription] [nvarchar](500) NULL,
	[Notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_EveOnlineAttribute] PRIMARY KEY CLUSTERED 
(
	[EveOnlineAttributeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Character]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Character](
	[CharacterId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CorporationId] [int] NOT NULL,
	[KeyId] [int] NOT NULL,
	[VCode] [varchar](200) NOT NULL,
	[CharacterName] [nvarchar](200) NOT NULL,
	[AddedDateTime] [datetime] NOT NULL,
	[ModifiedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Character] PRIMARY KEY CLUSTERED 
(
	[CharacterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactUsLog]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUsLog](
	[ContactUsLogId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[EmailAddress] [nvarchar](200) NULL,
	[Message] [nvarchar](max) NULL,
	[AddedByUserId] [int] NULL,
	[AddedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ContactUsLog] PRIMARY KEY CLUSTERED 
(
	[ContactUsLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineSkillTree]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineSkillTree](
	[EveOnlineSkillTreeId] [int] IDENTITY(1,1) NOT NULL,
	[Xml] [nvarchar](max) NOT NULL,
	[AddedByUserId] [int] NOT NULL,
	[AddedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EveOnlineSkillTree] PRIMARY KEY CLUSTERED 
(
	[EveOnlineSkillTreeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLog]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLog](
	[UserLogId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserLogTypeId] [int] NOT NULL,
	[ActionAgainstUserId] [int] NULL,
	[AddedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserLog] PRIMARY KEY CLUSTERED 
(
	[UserLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KillLog]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KillLog](
	[KillLogId] [int] NOT NULL,
	[SolarSystemId] [int] NOT NULL,
	[KillDateTime] [datetime] NOT NULL,
	[VictimCharacterId] [int] NOT NULL,
 CONSTRAINT [PK_KillLog] PRIMARY KEY CLUSTERED 
(
	[KillLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineGroup]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineGroup](
	[EveOnlineGroupId] [int] NOT NULL,
	[EveOnlineCategoryId] [int] NULL,
	[EveOnlineIconId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EveOnlineGroup] PRIMARY KEY CLUSTERED 
(
	[EveOnlineGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineRegion]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineRegion](
	[EveOnlineRegionId] [int] NOT NULL,
	[EveOnlineFactionId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_EveOnlineRegion] PRIMARY KEY CLUSTERED 
(
	[EveOnlineRegionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineBloodline]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineBloodline](
	[EveOnlineBloodlineId] [int] NOT NULL,
	[EveOnlineRaceId] [int] NOT NULL,
	[EveOnlineIconId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[MaleDescription] [nvarchar](max) NULL,
	[FemaleDescription] [nvarchar](max) NULL,
	[ShortDescription] [nvarchar](500) NULL,
	[ShortMaleDescription] [nvarchar](max) NULL,
	[ShortFemaleDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_EveOnlineBloodline] PRIMARY KEY CLUSTERED 
(
	[EveOnlineBloodlineId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineSkill]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineSkill](
	[EveOnlineSkillId] [int] NOT NULL,
	[EveOnlineSkillGroupId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Rank] [int] NOT NULL,
	[PrimaryEveOnlineAttributeId] [int] NOT NULL,
	[SecondaryEveOnlineAttributeId] [int] NOT NULL,
 CONSTRAINT [PK_EveOnlineSkill] PRIMARY KEY CLUSTERED 
(
	[EveOnlineSkillId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineRequiredSkill]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineRequiredSkill](
	[EveOnlineRequiredSkillId] [int] IDENTITY(1,1) NOT NULL,
	[EveOnlineSkillId] [int] NOT NULL,
	[RequiredEveOnlineSkillId] [int] NOT NULL,
	[RequiredSkillLevel] [int] NOT NULL,
 CONSTRAINT [PK_EveOnlineRequiredSkill] PRIMARY KEY CLUSTERED 
(
	[EveOnlineRequiredSkillId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineConstellation]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineConstellation](
	[EveOnlineConstellationId] [int] NOT NULL,
	[EveOnlineRegionId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_EveOnlineConstellation] PRIMARY KEY CLUSTERED 
(
	[EveOnlineConstellationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CharacterEveOnlineSkill]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CharacterEveOnlineSkill](
	[CharacterSkillId] [int] IDENTITY(1,1) NOT NULL,
	[CharacterId] [int] NOT NULL,
	[EveOnlineSkillId] [int] NOT NULL,
	[Rank] [tinyint] NOT NULL,
 CONSTRAINT [PK_CharacterEveOnlineSkill] PRIMARY KEY CLUSTERED 
(
	[CharacterSkillId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KillLogItem]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KillLogItem](
	[KillLogItemId] [int] IDENTITY(1,1) NOT NULL,
	[KillLogId] [int] NOT NULL,
	[ItemTypeId] [int] NOT NULL,
	[QuantityDropped] [int] NOT NULL,
	[QuantityDestroyed] [int] NOT NULL,
 CONSTRAINT [PK_KillLogItem] PRIMARY KEY CLUSTERED 
(
	[KillLogItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KillLogAttacker]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KillLogAttacker](
	[KillLogAttackerId] [int] IDENTITY(1,1) NOT NULL,
	[KillLogId] [int] NOT NULL,
	[CharacterId] [int] NOT NULL,
 CONSTRAINT [PK_KillLogAttacker] PRIMARY KEY CLUSTERED 
(
	[KillLogAttackerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineType]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineType](
	[EveOnlineTypeId] [int] NOT NULL,
	[EveOnlineGroupId] [int] NULL,
	[EveOnlineIconId] [int] NULL,
	[EveOnlineRaceId] [int] NULL,
	[EveOnlineMarketGroupId] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_EveOnlineType] PRIMARY KEY CLUSTERED 
(
	[EveOnlineTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EveOnlineSolarSystem]    Script Date: 01/13/2013 09:00:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EveOnlineSolarSystem](
	[EveOnlineSolarSystemId] [int] NOT NULL,
	[EveOnlineConstellationId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Security] [float] NOT NULL,
 CONSTRAINT [PK_EveOnlineSolarSystem] PRIMARY KEY CLUSTERED 
(
	[EveOnlineSolarSystemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_User_1_AddedByUser]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_1_AddedByUser] FOREIGN KEY([AddedByUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_1_AddedByUser]
GO
/****** Object:  ForeignKey [FK_User_2_ModifiedByUser]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_2_ModifiedByUser] FOREIGN KEY([ModifiedByUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_2_ModifiedByUser]
GO
/****** Object:  ForeignKey [FK_EveOnlineRace_EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineRace]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineRace_EveOnlineIcon] FOREIGN KEY([EveOnlineIconId])
REFERENCES [dbo].[EveOnlineIcon] ([EveOnlineIconId])
GO
ALTER TABLE [dbo].[EveOnlineRace] CHECK CONSTRAINT [FK_EveOnlineRace_EveOnlineIcon]
GO
/****** Object:  ForeignKey [FK_EveOnlineMarketGroup_EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineMarketGroup]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineMarketGroup_EveOnlineIcon] FOREIGN KEY([EveOnlineIconId])
REFERENCES [dbo].[EveOnlineIcon] ([EveOnlineIconId])
GO
ALTER TABLE [dbo].[EveOnlineMarketGroup] CHECK CONSTRAINT [FK_EveOnlineMarketGroup_EveOnlineIcon]
GO
/****** Object:  ForeignKey [FK_EveOnlineMarketGroup_EveOnlineMarketGroup]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineMarketGroup]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineMarketGroup_EveOnlineMarketGroup] FOREIGN KEY([ParentEveOnlineMarketGroupId])
REFERENCES [dbo].[EveOnlineMarketGroup] ([EveOnlineMarketGroupId])
GO
ALTER TABLE [dbo].[EveOnlineMarketGroup] CHECK CONSTRAINT [FK_EveOnlineMarketGroup_EveOnlineMarketGroup]
GO
/****** Object:  ForeignKey [FK_EveOnlineFaction_EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineFaction]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineFaction_EveOnlineIcon] FOREIGN KEY([EveOnlineIconId])
REFERENCES [dbo].[EveOnlineIcon] ([EveOnlineIconId])
GO
ALTER TABLE [dbo].[EveOnlineFaction] CHECK CONSTRAINT [FK_EveOnlineFaction_EveOnlineIcon]
GO
/****** Object:  ForeignKey [FK_EveOnlineCategory_EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineCategory]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineCategory_EveOnlineIcon] FOREIGN KEY([EveOnlineIconId])
REFERENCES [dbo].[EveOnlineIcon] ([EveOnlineIconId])
GO
ALTER TABLE [dbo].[EveOnlineCategory] CHECK CONSTRAINT [FK_EveOnlineCategory_EveOnlineIcon]
GO
/****** Object:  ForeignKey [FK_EveOnlineAttribute_EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineAttribute]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineAttribute_EveOnlineIcon] FOREIGN KEY([EveOnlineIconId])
REFERENCES [dbo].[EveOnlineIcon] ([EveOnlineIconId])
GO
ALTER TABLE [dbo].[EveOnlineAttribute] CHECK CONSTRAINT [FK_EveOnlineAttribute_EveOnlineIcon]
GO
/****** Object:  ForeignKey [FK_Character_Corporation]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[Character]  WITH CHECK ADD  CONSTRAINT [FK_Character_Corporation] FOREIGN KEY([CorporationId])
REFERENCES [dbo].[Corporation] ([CorporationId])
GO
ALTER TABLE [dbo].[Character] CHECK CONSTRAINT [FK_Character_Corporation]
GO
/****** Object:  ForeignKey [FK_Character_User]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[Character]  WITH CHECK ADD  CONSTRAINT [FK_Character_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Character] CHECK CONSTRAINT [FK_Character_User]
GO
/****** Object:  ForeignKey [FK_ContactUsLog_User]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[ContactUsLog]  WITH CHECK ADD  CONSTRAINT [FK_ContactUsLog_User] FOREIGN KEY([AddedByUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ContactUsLog] CHECK CONSTRAINT [FK_ContactUsLog_User]
GO
/****** Object:  ForeignKey [FK_EveOnlineSkillTree_User]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineSkillTree]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineSkillTree_User] FOREIGN KEY([AddedByUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[EveOnlineSkillTree] CHECK CONSTRAINT [FK_EveOnlineSkillTree_User]
GO
/****** Object:  ForeignKey [FK_UserRole_Role]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
/****** Object:  ForeignKey [FK_UserRole_User]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
/****** Object:  ForeignKey [FK_UserLog_1_User]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[UserLog]  WITH CHECK ADD  CONSTRAINT [FK_UserLog_1_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserLog] CHECK CONSTRAINT [FK_UserLog_1_User]
GO
/****** Object:  ForeignKey [FK_UserLog_2_ActionAgainstUser]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[UserLog]  WITH CHECK ADD  CONSTRAINT [FK_UserLog_2_ActionAgainstUser] FOREIGN KEY([ActionAgainstUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserLog] CHECK CONSTRAINT [FK_UserLog_2_ActionAgainstUser]
GO
/****** Object:  ForeignKey [FK_UserLog_UserLogType]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[UserLog]  WITH CHECK ADD  CONSTRAINT [FK_UserLog_UserLogType] FOREIGN KEY([UserLogTypeId])
REFERENCES [dbo].[UserLogType] ([UserLogTypeId])
GO
ALTER TABLE [dbo].[UserLog] CHECK CONSTRAINT [FK_UserLog_UserLogType]
GO
/****** Object:  ForeignKey [FK_KillLog_Character]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[KillLog]  WITH CHECK ADD  CONSTRAINT [FK_KillLog_Character] FOREIGN KEY([VictimCharacterId])
REFERENCES [dbo].[Character] ([CharacterId])
GO
ALTER TABLE [dbo].[KillLog] CHECK CONSTRAINT [FK_KillLog_Character]
GO
/****** Object:  ForeignKey [FK_EveOnlineGroup_EveOnlineCategory]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineGroup]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineGroup_EveOnlineCategory] FOREIGN KEY([EveOnlineCategoryId])
REFERENCES [dbo].[EveOnlineCategory] ([EveOnlineCategoryId])
GO
ALTER TABLE [dbo].[EveOnlineGroup] CHECK CONSTRAINT [FK_EveOnlineGroup_EveOnlineCategory]
GO
/****** Object:  ForeignKey [FK_EveOnlineGroup_EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineGroup]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineGroup_EveOnlineIcon] FOREIGN KEY([EveOnlineIconId])
REFERENCES [dbo].[EveOnlineIcon] ([EveOnlineIconId])
GO
ALTER TABLE [dbo].[EveOnlineGroup] CHECK CONSTRAINT [FK_EveOnlineGroup_EveOnlineIcon]
GO
/****** Object:  ForeignKey [FK_EveOnlineRegion_EveOnlineFaction]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineRegion]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineRegion_EveOnlineFaction] FOREIGN KEY([EveOnlineFactionId])
REFERENCES [dbo].[EveOnlineFaction] ([EveOnlineFactionId])
GO
ALTER TABLE [dbo].[EveOnlineRegion] CHECK CONSTRAINT [FK_EveOnlineRegion_EveOnlineFaction]
GO
/****** Object:  ForeignKey [FK_EveOnlineBloodline_EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineBloodline]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineBloodline_EveOnlineIcon] FOREIGN KEY([EveOnlineIconId])
REFERENCES [dbo].[EveOnlineIcon] ([EveOnlineIconId])
GO
ALTER TABLE [dbo].[EveOnlineBloodline] CHECK CONSTRAINT [FK_EveOnlineBloodline_EveOnlineIcon]
GO
/****** Object:  ForeignKey [FK_EveOnlineBloodline_EveOnlineRace]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineBloodline]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineBloodline_EveOnlineRace] FOREIGN KEY([EveOnlineRaceId])
REFERENCES [dbo].[EveOnlineRace] ([EveOnlineRaceId])
GO
ALTER TABLE [dbo].[EveOnlineBloodline] CHECK CONSTRAINT [FK_EveOnlineBloodline_EveOnlineRace]
GO
/****** Object:  ForeignKey [FK_EveOnlineSkill_1_PrimaryEveOnlineAttribute]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineSkill]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineSkill_1_PrimaryEveOnlineAttribute] FOREIGN KEY([PrimaryEveOnlineAttributeId])
REFERENCES [dbo].[EveOnlineAttribute] ([EveOnlineAttributeId])
GO
ALTER TABLE [dbo].[EveOnlineSkill] CHECK CONSTRAINT [FK_EveOnlineSkill_1_PrimaryEveOnlineAttribute]
GO
/****** Object:  ForeignKey [FK_EveOnlineSkill_2_SecondaryEveOnlineAttribute]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineSkill]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineSkill_2_SecondaryEveOnlineAttribute] FOREIGN KEY([SecondaryEveOnlineAttributeId])
REFERENCES [dbo].[EveOnlineAttribute] ([EveOnlineAttributeId])
GO
ALTER TABLE [dbo].[EveOnlineSkill] CHECK CONSTRAINT [FK_EveOnlineSkill_2_SecondaryEveOnlineAttribute]
GO
/****** Object:  ForeignKey [FK_EveOnlineSkill_EveOnlineSkillGroup]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineSkill]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineSkill_EveOnlineSkillGroup] FOREIGN KEY([EveOnlineSkillGroupId])
REFERENCES [dbo].[EveOnlineSkillGroup] ([EveOnlineSkillGroupId])
GO
ALTER TABLE [dbo].[EveOnlineSkill] CHECK CONSTRAINT [FK_EveOnlineSkill_EveOnlineSkillGroup]
GO
/****** Object:  ForeignKey [FK_EveOnlineRequiredSkill_1_EveOnlineSkill]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineRequiredSkill]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineRequiredSkill_1_EveOnlineSkill] FOREIGN KEY([EveOnlineSkillId])
REFERENCES [dbo].[EveOnlineSkill] ([EveOnlineSkillId])
GO
ALTER TABLE [dbo].[EveOnlineRequiredSkill] CHECK CONSTRAINT [FK_EveOnlineRequiredSkill_1_EveOnlineSkill]
GO
/****** Object:  ForeignKey [FK_EveOnlineRequiredSkill_2_RequiredEveOnlineSkill]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineRequiredSkill]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineRequiredSkill_2_RequiredEveOnlineSkill] FOREIGN KEY([RequiredEveOnlineSkillId])
REFERENCES [dbo].[EveOnlineSkill] ([EveOnlineSkillId])
GO
ALTER TABLE [dbo].[EveOnlineRequiredSkill] CHECK CONSTRAINT [FK_EveOnlineRequiredSkill_2_RequiredEveOnlineSkill]
GO
/****** Object:  ForeignKey [FK_EveOnlineConstellation_EveOnlineRegion]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineConstellation]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineConstellation_EveOnlineRegion] FOREIGN KEY([EveOnlineRegionId])
REFERENCES [dbo].[EveOnlineRegion] ([EveOnlineRegionId])
GO
ALTER TABLE [dbo].[EveOnlineConstellation] CHECK CONSTRAINT [FK_EveOnlineConstellation_EveOnlineRegion]
GO
/****** Object:  ForeignKey [FK_CharacterEveOnlineSkill_Character]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[CharacterEveOnlineSkill]  WITH CHECK ADD  CONSTRAINT [FK_CharacterEveOnlineSkill_Character] FOREIGN KEY([CharacterId])
REFERENCES [dbo].[Character] ([CharacterId])
GO
ALTER TABLE [dbo].[CharacterEveOnlineSkill] CHECK CONSTRAINT [FK_CharacterEveOnlineSkill_Character]
GO
/****** Object:  ForeignKey [FK_CharacterEveOnlineSkill_EveOnlineSkill]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[CharacterEveOnlineSkill]  WITH CHECK ADD  CONSTRAINT [FK_CharacterEveOnlineSkill_EveOnlineSkill] FOREIGN KEY([EveOnlineSkillId])
REFERENCES [dbo].[EveOnlineSkill] ([EveOnlineSkillId])
GO
ALTER TABLE [dbo].[CharacterEveOnlineSkill] CHECK CONSTRAINT [FK_CharacterEveOnlineSkill_EveOnlineSkill]
GO
/****** Object:  ForeignKey [FK_KillLogItem_KillLog]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[KillLogItem]  WITH CHECK ADD  CONSTRAINT [FK_KillLogItem_KillLog] FOREIGN KEY([KillLogId])
REFERENCES [dbo].[KillLog] ([KillLogId])
GO
ALTER TABLE [dbo].[KillLogItem] CHECK CONSTRAINT [FK_KillLogItem_KillLog]
GO
/****** Object:  ForeignKey [FK_KillLogAttacker_KillLog]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[KillLogAttacker]  WITH CHECK ADD  CONSTRAINT [FK_KillLogAttacker_KillLog] FOREIGN KEY([KillLogId])
REFERENCES [dbo].[KillLog] ([KillLogId])
GO
ALTER TABLE [dbo].[KillLogAttacker] CHECK CONSTRAINT [FK_KillLogAttacker_KillLog]
GO
/****** Object:  ForeignKey [FK_EveOnlineType_EveOnlineGroup]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineType]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineType_EveOnlineGroup] FOREIGN KEY([EveOnlineGroupId])
REFERENCES [dbo].[EveOnlineGroup] ([EveOnlineGroupId])
GO
ALTER TABLE [dbo].[EveOnlineType] CHECK CONSTRAINT [FK_EveOnlineType_EveOnlineGroup]
GO
/****** Object:  ForeignKey [FK_EveOnlineType_EveOnlineIcon]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineType]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineType_EveOnlineIcon] FOREIGN KEY([EveOnlineIconId])
REFERENCES [dbo].[EveOnlineIcon] ([EveOnlineIconId])
GO
ALTER TABLE [dbo].[EveOnlineType] CHECK CONSTRAINT [FK_EveOnlineType_EveOnlineIcon]
GO
/****** Object:  ForeignKey [FK_EveOnlineType_EveOnlineMarketGroup]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineType]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineType_EveOnlineMarketGroup] FOREIGN KEY([EveOnlineMarketGroupId])
REFERENCES [dbo].[EveOnlineMarketGroup] ([EveOnlineMarketGroupId])
GO
ALTER TABLE [dbo].[EveOnlineType] CHECK CONSTRAINT [FK_EveOnlineType_EveOnlineMarketGroup]
GO
/****** Object:  ForeignKey [FK_EveOnlineType_EveOnlineRace]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineType]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineType_EveOnlineRace] FOREIGN KEY([EveOnlineRaceId])
REFERENCES [dbo].[EveOnlineRace] ([EveOnlineRaceId])
GO
ALTER TABLE [dbo].[EveOnlineType] CHECK CONSTRAINT [FK_EveOnlineType_EveOnlineRace]
GO
/****** Object:  ForeignKey [FK_EveOnlineSolarSystem_EveOnlineConstellation]    Script Date: 01/13/2013 09:00:04 ******/
ALTER TABLE [dbo].[EveOnlineSolarSystem]  WITH CHECK ADD  CONSTRAINT [FK_EveOnlineSolarSystem_EveOnlineConstellation] FOREIGN KEY([EveOnlineConstellationId])
REFERENCES [dbo].[EveOnlineConstellation] ([EveOnlineConstellationId])
GO
ALTER TABLE [dbo].[EveOnlineSolarSystem] CHECK CONSTRAINT [FK_EveOnlineSolarSystem_EveOnlineConstellation]
GO
