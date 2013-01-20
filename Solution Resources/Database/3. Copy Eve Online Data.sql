USE FallenNovaDevelopment
GO

DELETE FROM EveOnlineSolarSystem
DELETE FROM EveOnlineConstellation
DELETE FROM EveOnlineRegion
DELETE FROM EveOnlineAttribute
DELETE FROM EveOnlineBloodline
DELETE FROM EveOnlineFaction
DELETE FROM EveOnlineType
DELETE FROM EveOnlineGroup
DELETE FROM EveOnlineCategory
DELETE FROM EveOnlineMarketGroup
DELETE FROM EveOnlineRace
DELETE FROM EveOnlineIcon

USE EveOnline
GO

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineIcon 
	(EveOnlineIconId, FileNameWithoutExtension, [Description])
SELECT  
	eveIcons.iconID, 
	eveIcons.iconFile, 
	eveIcons.[description]  
FROM    
	eveIcons

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineAttribute 
	(EveOnlineAttributeId, EveOnlineIconId, Name, [Description], ShortDescription, Notes)
SELECT  
	chrAttributes.attributeID, 
	chrAttributes.iconID, 
	chrAttributes.attributeName, 
	chrAttributes.[description], 
	chrAttributes.shortDescription, 
	chrAttributes.notes
FROM    
	chrAttributes

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineRace 
	(EveOnlineRaceId, EveOnlineIconId, Name, [Description], ShortDescription)
SELECT  
	chrRaces.raceID, 
	chrRaces.iconID, 
	chrRaces.raceName, 
	chrRaces.[description], 
	chrRaces.shortDescription 
FROM    
	chrRaces

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineFaction 
	(EveOnlineFactionId, EveOnlineIconId, Name, [Description])
SELECT  
	chrFactions.factionID, 
	chrFactions.iconID, 
	chrFactions.factionName, 
	chrFactions.[description] 
FROM    
	chrFactions

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineBloodline 
	(EveOnlineBloodlineId, EveOnlineRaceId, EveOnlineIconId, Name, [Description], MaleDescription, FemaleDescription, ShortDescription, ShortMaleDescription, ShortFemaleDescription)
SELECT  
	chrBloodlines.bloodlineID, 
	chrBloodlines.raceID, 
	chrBloodlines.iconID, 
	chrBloodlines.bloodlineName, 
	chrBloodlines.[description], 
	chrBloodlines.maleDescription, 
	chrBloodlines.femaleDescription, 
	chrBloodlines.shortDescription, 
	chrBloodlines.shortMaleDescription, 
	chrBloodlines.shortFemaleDescription
FROM    
	chrBloodlines

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineMarketGroup 
	(EveOnlineMarketGroupId, ParentEveOnlineMarketGroupId, EveOnlineIconId, Name, [Description], HasTypes)
SELECT  
	invMarketGroups.marketGroupID, 
	invMarketGroups.parentGroupID, 
	invMarketGroups.iconID, 
	invMarketGroups.marketGroupName, 
	invMarketGroups.[description], 
	invMarketGroups.hasTypes 
FROM    
	invMarketGroups

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineCategory 
	(EveOnlineCategoryId, EveOnlineIconId, Name, [Description])
SELECT  
	invCategories.categoryID, 
	invCategories.iconID, 
	invCategories.categoryName, 
	invCategories.[description] 
FROM    
	invCategories

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineGroup 
	(EveOnlineGroupId, EveOnlineCategoryId, EveOnlineIconId, Name, [Description])
SELECT  
	invGroups.groupID, 
	invGroups.categoryID, 
	invGroups.iconID, 
	invGroups.groupName, 
	invGroups.[description] 
FROM    
	invGroups

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineType 
	(EveOnlineTypeId, EveOnlineGroupId, EveOnlineIconId, EveOnlineRaceId, EveOnlineMarketGroupId, Name, [Description])
SELECT  
	invTypes.typeID, 
	invTypes.groupID, 
	invTypes.iconID, 
	invTypes.raceID, 
	invTypes.marketGroupID, 
	invTypes.typeName, 
	invTypes.[description] 
FROM    
	invTypes

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineRegion 
	(EveOnlineRegionId, EveOnlineFactionId, Name)
SELECT  
	mapRegions.regionID, 
	mapRegions.factionID, 
	mapRegions.regionName
FROM    
	mapRegions

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineConstellation 
	(EveOnlineConstellationId, EveOnlineRegionId, Name)
SELECT  
	mapConstellations.constellationID, 
	mapConstellations.regionID, 
	mapConstellations.constellationName
FROM    
	mapConstellations

INSERT INTO FallenNovaDevelopment.dbo.EveOnlineSolarSystem 
	(EveOnlineSolarSystemId, EveOnlineConstellationId, Name, [Security])
SELECT  
	mapSolarSystems.solarSystemID, 
	mapSolarSystems.constellationID, 
	mapSolarSystems.solarSystemName, 
	mapSolarSystems.[security]
FROM    
	mapSolarSystems
