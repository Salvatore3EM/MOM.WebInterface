CREATE TABLE SynopticLayouts (
    Layout NVARCHAR(255) NOT NULL PRIMARY KEY,
    AreaId NVARCHAR(255) NULL,
    Svg NVARCHAR(MAX) NULL,
    SvgBackup NVARCHAR(MAX) NULL
);

-- Creazione della tabella SynopticData di mock
CREATE TABLE SynopticData (
    ElementId NVARCHAR(50) NOT NULL,
    SynopticLayout NVARCHAR(255) NOT NULL,
    Text1 NVARCHAR(MAX) NULL,
    Text2 NVARCHAR(MAX) NULL,
    Text3 NVARCHAR(MAX) NULL,
    Status INT NOT NULL DEFAULT(0),
    LastUpdate DATETIME NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT PK_SynopticData PRIMARY KEY (ElementId, SynopticLayout),
    CONSTRAINT FK_SynopticData_Layout FOREIGN KEY (SynopticLayout) 
        REFERENCES SynopticLayouts(Layout)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);