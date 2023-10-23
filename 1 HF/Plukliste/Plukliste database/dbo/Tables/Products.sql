CREATE TABLE [dbo].[Products] (
    [ProduktNr] NVARCHAR (50)  NOT NULL,
    [Type]      NVARCHAR (50)  NULL,
    [Navn]      NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ProduktNr] ASC)
);

