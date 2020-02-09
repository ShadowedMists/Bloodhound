CREATE TABLE [dbo].[Offenders] (
    [OffenderId]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [OffenderName]    NVARCHAR (128)  NOT NULL,
    [OffenderSummary] NVARCHAR (2048) NOT NULL,
    CONSTRAINT [PK_Offenders] PRIMARY KEY CLUSTERED ([OffenderId] ASC)
);

