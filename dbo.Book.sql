CREATE TABLE [dbo].Book
(
	[ISBN] INT NOT NULL IDENTITY , 
    [Title] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] NUMERIC(5, 2) NULL, 
    [Specialization] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Book] PRIMARY KEY ([ISBN])
)
