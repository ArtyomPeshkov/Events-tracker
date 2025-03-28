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
CREATE TABLE [Categories] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);

CREATE TABLE [Users] (
    [Id] bigint NOT NULL IDENTITY,
    [Login] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [Events] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [EventType] nvarchar(13) NOT NULL,
    [DeletionDate] datetime2 NULL,
    [NextNotificationDate] datetime2 NULL,
    [ProlongPeriod] bigint NULL,
    [DateShift] bigint NULL,
    [IsRegular] bit NULL,
    [IsExtendable] bit NULL,
    [UserId] bigint NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Events_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Profiles] (
    [Id] bigint NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_Profiles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Profiles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [EventCategories] (
    [EventId] bigint NOT NULL,
    [CategoryId] bigint NOT NULL,
    CONSTRAINT [PK_EventCategories] PRIMARY KEY ([EventId], [CategoryId]),
    CONSTRAINT [FK_EventCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EventCategories_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_EventCategories_CategoryId] ON [EventCategories] ([CategoryId]);

CREATE INDEX [IX_Events_UserId] ON [Events] ([UserId]);

CREATE UNIQUE INDEX [IX_Profiles_UserId] ON [Profiles] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250328144245_first', N'9.0.3');

DROP TABLE [Profiles];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250328144518_second', N'9.0.3');

COMMIT;
GO

