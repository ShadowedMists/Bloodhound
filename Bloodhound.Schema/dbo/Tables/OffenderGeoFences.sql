CREATE TABLE [dbo].[OffenderGeoFences] (
    [OffenderGeoFenceId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [GeoFenceName]       NVARCHAR (128)  NOT NULL,
    [OffenderId]         BIGINT          NOT NULL,
    [GeoFenceTypeId]     INT             NOT NULL,
    [Address]            NVARCHAR (256)  NOT NULL,
    [NorthEastLatitude]  DECIMAL (12, 6) NOT NULL,
    [NorthEastLongitude] DECIMAL (12, 6) NOT NULL,
    [SouthWestLatitude]  DECIMAL (12, 6) NOT NULL,
    [SouthWestLongitude] DECIMAL (12, 6) NOT NULL,
    CONSTRAINT [PK_OffenderGeoFences] PRIMARY KEY CLUSTERED ([OffenderGeoFenceId] ASC),
    CONSTRAINT [FK_OffenderGeoFences_GeoFenceTypes_GeoFenceTypeId] FOREIGN KEY ([GeoFenceTypeId]) REFERENCES [dbo].[GeoFenceTypes] ([GeoFenceTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OffenderGeoFences_Offenders_OffenderId] FOREIGN KEY ([OffenderId]) REFERENCES [dbo].[Offenders] ([OffenderId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_OffenderGeoFences_OffenderId]
    ON [dbo].[OffenderGeoFences]([OffenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OffenderGeoFences_GeoFenceTypeId]
    ON [dbo].[OffenderGeoFences]([GeoFenceTypeId] ASC);

