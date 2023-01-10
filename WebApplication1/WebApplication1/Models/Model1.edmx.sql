
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/08/2023 00:01:48
-- Generated from EDMX file: C:\Users\lenovo\Documents\GitHub\Dotnet_project\WebApplication1\WebApplication1\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [profinsem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Bibliothe__id_ap__44FF419A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bibliotheque_app] DROP CONSTRAINT [FK__Bibliothe__id_ap__44FF419A];
GO
IF OBJECT_ID(N'[dbo].[FK__Bibliothe__id_bi__45F365D3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bibliotheque_app] DROP CONSTRAINT [FK__Bibliothe__id_bi__45F365D3];
GO
IF OBJECT_ID(N'[dbo].[FK__bibliothe__id_us__40F9A68C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[bibliotheque] DROP CONSTRAINT [FK__bibliothe__id_us__40F9A68C];
GO
IF OBJECT_ID(N'[dbo].[FK__review__id_app__3F466844]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[review] DROP CONSTRAINT [FK__review__id_app__3F466844];
GO
IF OBJECT_ID(N'[dbo].[FK__review__id_user__403A8C7D]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[review] DROP CONSTRAINT [FK__review__id_user__403A8C7D];
GO
IF OBJECT_ID(N'[dbo].[FK_utilisateurbibliotheque]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[utilisateur] DROP CONSTRAINT [FK_utilisateurbibliotheque];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[bibliotheque]', 'U') IS NOT NULL
    DROP TABLE [dbo].[bibliotheque];
GO
IF OBJECT_ID(N'[dbo].[Bibliotheque_app]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bibliotheque_app];
GO
IF OBJECT_ID(N'[dbo].[categorie]', 'U') IS NOT NULL
    DROP TABLE [dbo].[categorie];
GO
IF OBJECT_ID(N'[dbo].[review]', 'U') IS NOT NULL
    DROP TABLE [dbo].[review];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[u_application]', 'U') IS NOT NULL
    DROP TABLE [dbo].[u_application];
GO
IF OBJECT_ID(N'[dbo].[utilisateur]', 'U') IS NOT NULL
    DROP TABLE [dbo].[utilisateur];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'bibliotheque'
CREATE TABLE [dbo].[bibliotheque] (
    [id_bib] int IDENTITY(1,1) NOT NULL,
    [id_user] int  NOT NULL,
    [utilisateur_id_user] int  NOT NULL
);
GO

-- Creating table 'Bibliotheque_app'
CREATE TABLE [dbo].[Bibliotheque_app] (
    [id_app] int  NOT NULL,
    [id_bib] int  NOT NULL,
    [date_telechargement] datetime  NULL
);
GO

-- Creating table 'categorie'
CREATE TABLE [dbo].[categorie] (
    [id_cat] int IDENTITY(1,1) NOT NULL,
    [nom_categorie] varchar(128)  NULL
);
GO

-- Creating table 'review'
CREATE TABLE [dbo].[review] (
    [id_review] int IDENTITY(1,1) NOT NULL,
    [Commentaire] varchar(512)  NOT NULL,
    [id_app] int  NOT NULL,
    [id_user] int  NULL,
    [note] float  NULL
);
GO

-- Creating table 'u_application'
CREATE TABLE [dbo].[u_application] (
    [id_app] int IDENTITY(1,1) NOT NULL,
    [nom_app] varchar(64)  NOT NULL,
    [nombre_telechargement] int  NULL,
    [date_ajout] datetime  NULL,
    [categorie] int  NULL,
    [id_user] int  NULL,
    [app_img] varchar(max)  NULL,
    [u_description] varchar(max)  NULL,
    [categorie_id_cat] int  NOT NULL
);
GO

-- Creating table 'utilisateur'
CREATE TABLE [dbo].[utilisateur] (
    [id_user] int IDENTITY(1,1) NOT NULL,
    [nom_utilisateur] varchar(50)  NOT NULL,
    [email] varchar(128)  NOT NULL,
    [nom] varchar(50)  NULL,
    [prenom] varchar(50)  NULL,
    [date_naissance] datetime  NOT NULL,
    [motdepasse] varchar(64)  NOT NULL,
    [u_role] varchar(32)  NULL,
    [bibliotheque_id_bib] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id_bib] in table 'bibliotheque'
ALTER TABLE [dbo].[bibliotheque]
ADD CONSTRAINT [PK_bibliotheque]
    PRIMARY KEY CLUSTERED ([id_bib] ASC);
GO

-- Creating primary key on [id_app], [id_bib] in table 'Bibliotheque_app'
ALTER TABLE [dbo].[Bibliotheque_app]
ADD CONSTRAINT [PK_Bibliotheque_app]
    PRIMARY KEY CLUSTERED ([id_app], [id_bib] ASC);
GO

-- Creating primary key on [id_cat] in table 'categorie'
ALTER TABLE [dbo].[categorie]
ADD CONSTRAINT [PK_categorie]
    PRIMARY KEY CLUSTERED ([id_cat] ASC);
GO

-- Creating primary key on [id_review] in table 'review'
ALTER TABLE [dbo].[review]
ADD CONSTRAINT [PK_review]
    PRIMARY KEY CLUSTERED ([id_review] ASC);
GO

-- Creating primary key on [id_app] in table 'u_application'
ALTER TABLE [dbo].[u_application]
ADD CONSTRAINT [PK_u_application]
    PRIMARY KEY CLUSTERED ([id_app] ASC);
GO

-- Creating primary key on [id_user] in table 'utilisateur'
ALTER TABLE [dbo].[utilisateur]
ADD CONSTRAINT [PK_utilisateur]
    PRIMARY KEY CLUSTERED ([id_user] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_bib] in table 'Bibliotheque_app'
ALTER TABLE [dbo].[Bibliotheque_app]
ADD CONSTRAINT [FK__Bibliothe__id_bi__45F365D3]
    FOREIGN KEY ([id_bib])
    REFERENCES [dbo].[bibliotheque]
        ([id_bib])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Bibliothe__id_bi__45F365D3'
CREATE INDEX [IX_FK__Bibliothe__id_bi__45F365D3]
ON [dbo].[Bibliotheque_app]
    ([id_bib]);
GO

-- Creating foreign key on [id_app] in table 'Bibliotheque_app'
ALTER TABLE [dbo].[Bibliotheque_app]
ADD CONSTRAINT [FK__Bibliothe__id_ap__44FF419A]
    FOREIGN KEY ([id_app])
    REFERENCES [dbo].[u_application]
        ([id_app])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [id_app] in table 'review'
ALTER TABLE [dbo].[review]
ADD CONSTRAINT [FK__review__id_app__3F466844]
    FOREIGN KEY ([id_app])
    REFERENCES [dbo].[u_application]
        ([id_app])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__review__id_app__3F466844'
CREATE INDEX [IX_FK__review__id_app__3F466844]
ON [dbo].[review]
    ([id_app]);
GO

-- Creating foreign key on [id_user] in table 'review'
ALTER TABLE [dbo].[review]
ADD CONSTRAINT [FK__review__id_user__403A8C7D]
    FOREIGN KEY ([id_user])
    REFERENCES [dbo].[utilisateur]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__review__id_user__403A8C7D'
CREATE INDEX [IX_FK__review__id_user__403A8C7D]
ON [dbo].[review]
    ([id_user]);
GO

-- Creating foreign key on [utilisateur_id_user] in table 'bibliotheque'
ALTER TABLE [dbo].[bibliotheque]
ADD CONSTRAINT [FK_bibliothequeutilisateur]
    FOREIGN KEY ([utilisateur_id_user])
    REFERENCES [dbo].[utilisateur]
        ([id_user])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_bibliothequeutilisateur'
CREATE INDEX [IX_FK_bibliothequeutilisateur]
ON [dbo].[bibliotheque]
    ([utilisateur_id_user]);
GO

-- Creating foreign key on [categorie_id_cat] in table 'u_application'
ALTER TABLE [dbo].[u_application]
ADD CONSTRAINT [FK_categorieu_application]
    FOREIGN KEY ([categorie_id_cat])
    REFERENCES [dbo].[categorie]
        ([id_cat])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_categorieu_application'
CREATE INDEX [IX_FK_categorieu_application]
ON [dbo].[u_application]
    ([categorie_id_cat]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------