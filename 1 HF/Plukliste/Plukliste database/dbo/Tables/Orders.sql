CREATE TABLE [dbo].[Orders] (
    [OrderID]    INT           NOT NULL,
    [CustomerID] INT           NULL,
    [ProduktNr]  NVARCHAR (50) NULL,
    [Quantity]   INT           NULL,
    PRIMARY KEY CLUSTERED ([OrderID] ASC),
    FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID]),
    FOREIGN KEY ([ProduktNr]) REFERENCES [dbo].[Products] ([ProduktNr])
);

