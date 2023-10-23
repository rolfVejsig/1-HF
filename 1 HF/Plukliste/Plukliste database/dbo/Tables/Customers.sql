CREATE TABLE [dbo].[Customers] (
    [CustomerID]  INT            NOT NULL,
    [Name]        NVARCHAR (255) NULL,
    [Forsendelse] NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

