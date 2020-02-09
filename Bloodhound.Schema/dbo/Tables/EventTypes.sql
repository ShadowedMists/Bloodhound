CREATE TABLE [dbo].[EventTypes] (
    [EventTypeId]   INT            IDENTITY (1, 1) NOT NULL,
    [EventTypeName] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_EventTypes] PRIMARY KEY CLUSTERED ([EventTypeId] ASC)
);

