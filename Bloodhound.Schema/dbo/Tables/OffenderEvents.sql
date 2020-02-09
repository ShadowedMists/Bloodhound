CREATE TABLE [dbo].[OffenderEvents] (
    [OffenderEventId]    BIGINT             IDENTITY (1, 1) NOT NULL,
    [OffenderId]         BIGINT             NOT NULL,
    [EventTypeId]        INT                NOT NULL,
    [OffenderLocationId] BIGINT             NULL,
    [OffenderGeoFenceId] BIGINT             NULL,
    [CreatedOn]          DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_OffenderEvents] PRIMARY KEY CLUSTERED ([OffenderEventId] ASC),
    CONSTRAINT [FK_OffenderEvents_EventTypes_EventTypeId] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventTypes] ([EventTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OffenderEvents_OffenderGeoFences_OffenderGeoFenceId] FOREIGN KEY ([OffenderGeoFenceId]) REFERENCES [dbo].[OffenderGeoFences] ([OffenderGeoFenceId]),
    CONSTRAINT [FK_OffenderEvents_OffenderLocations_OffenderLocationId] FOREIGN KEY ([OffenderLocationId]) REFERENCES [dbo].[OffenderLocations] ([OffenderLocationId]),
    CONSTRAINT [FK_OffenderEvents_Offenders_OffenderId] FOREIGN KEY ([OffenderId]) REFERENCES [dbo].[Offenders] ([OffenderId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_OffenderEvents_OffenderLocationId]
    ON [dbo].[OffenderEvents]([OffenderLocationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OffenderEvents_OffenderId]
    ON [dbo].[OffenderEvents]([OffenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OffenderEvents_OffenderGeoFenceId]
    ON [dbo].[OffenderEvents]([OffenderGeoFenceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OffenderEvents_EventTypeId]
    ON [dbo].[OffenderEvents]([EventTypeId] ASC);

