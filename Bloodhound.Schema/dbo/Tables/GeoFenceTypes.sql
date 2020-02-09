CREATE TABLE [dbo].[GeoFenceTypes] (
    [GeoFenceTypeId]   INT            IDENTITY (1, 1) NOT NULL,
    [GeoFenceTypeName] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_GeoFenceTypes] PRIMARY KEY CLUSTERED ([GeoFenceTypeId] ASC)
);

