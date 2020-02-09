CREATE TABLE [dbo].[OffenderLocations] (
    [OffenderLocationId] BIGINT             IDENTITY (1, 1) NOT NULL,
    [OffenderId]         BIGINT             NOT NULL,
    [Latitude]           DECIMAL (12, 6)    NOT NULL,
    [Longitude]          DECIMAL (12, 6)    NOT NULL,
    [LocationTime]       DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_OffenderLocations] PRIMARY KEY CLUSTERED ([OffenderLocationId] ASC),
    CONSTRAINT [FK_OffenderLocations_Offenders_OffenderId] FOREIGN KEY ([OffenderId]) REFERENCES [dbo].[Offenders] ([OffenderId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_OffenderLocations_OffenderId]
    ON [dbo].[OffenderLocations]([OffenderId] ASC);

