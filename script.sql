IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Conversions] (
    [ConversionId] uniqueidentifier NOT NULL,
    [ConversionDate] datetime2 NOT NULL,
    [ActualConversion_Amount] decimal(18,2) NOT NULL,
    [ActualConversion_Currency] nvarchar(3) NOT NULL,
    CONSTRAINT [PK_Conversions] PRIMARY KEY ([ConversionId])
);

CREATE TABLE [CryptoHistoryRecords] (
    [HistoryId] uniqueidentifier NOT NULL,
    [HistoryDate] datetime2 NOT NULL,
    [OriginalPrice_Amount] decimal(18,2) NOT NULL,
    [OriginalPrice_Currency] nvarchar(3) NOT NULL,
    [ConvertedPrice_Amount] decimal(18,2) NOT NULL,
    [ConvertedPrice_Currency] nvarchar(3) NOT NULL,
    [Comment] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_CryptoHistoryRecords] PRIMARY KEY ([HistoryId])
);

CREATE TABLE [DomainEvent] (
    [DomainEventId] uniqueidentifier NOT NULL,
    [OccuredAt] datetime2 NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [AssemblyName] nvarchar(max) NOT NULL,
    [Payload] nvarchar(max) NOT NULL,
    [CompletedAt] datetime2 NULL,
    CONSTRAINT [PK_DomainEvent] PRIMARY KEY ([DomainEventId])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250314165528_TargetCurrency-history', N'9.0.3');

COMMIT;
GO

