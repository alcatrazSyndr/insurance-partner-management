CREATE TABLE IF NOT EXISTS Partners (
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
	FirstName TEXT NOT NULL CHECK(length(FirstName) >= 2 AND length(FirstName) <= 255),
	LastName TEXT NOT NULL CHECK(length(LastName) >= 2 AND length(LastName) <= 255),
	Address TEXT,
	PartnerNumber TEXT NOT NULL CHECK(length(PartnerNumber) = 20 AND PartnerNumber NOT GLOB '*[^0-9]*'),
	CroatianPIN TEXT CHECK(CroatianPIN IS NULL OR (length(CroatianPIN) = 11 AND CroatianPIN NOT GLOB '*[^0-9]*')),
	PartnerTypeId INTEGER NOT NULL CHECK(PartnerTypeId IN (1, 2)),
	CreatedAtUtc DATETIME NOT NULL DEFAULT(datetime('now')),
	CreatedByUser TEXT NOT NULL CHECK(length(CreatedByUser) <= 255 AND CreatedByUser LIKE '%_@__%.__%'),
	IsForeign INTEGER NOT NULL CHECK(IsForeign IN (0, 1)),
ExternalCode TEXT CHECK(ExternalCode IS NULL OR (length(ExternalCode) >= 10 AND length(ExternalCode) <= 20)) UNIQUE,
	Gender TEXT NOT NULL CHECK(Gender IN ('M', 'F', 'N'))
);

CREATE TABLE IF NOT EXISTS Policies (
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
	PartnerId INTEGER NOT NULL,
	PolicyNumber TEXT NOT NULL CHECK(length(PolicyNumber) >= 10 AND length(PolicyNumber) <= 15),
	Amount NUMERIC(18,2) NOT NULL CHECK(Amount > 0),
	CreatedAtUtc DATETIME NOT NULL DEFAULT(datetime('now')),
	FOREIGN KEY (PartnerId) REFERENCES Partners(Id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS IX_Policies_PartnerId ON Policies(PartnerId);
CREATE INDEX IF NOT EXISTS IX_Partners_CreatedAtUtc ON Partners(CreatedAtUtc DESC);