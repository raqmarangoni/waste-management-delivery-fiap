-- SQLite schema for WasteManagement (for environments where dotnet tools aren't available)
CREATE TABLE IF NOT EXISTS Collections (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Location TEXT NOT NULL,
    CollectedAt TEXT NOT NULL,
    MaterialType TEXT,
    WeightKg REAL NOT NULL
);

CREATE TABLE IF NOT EXISTS Alerts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Message TEXT NOT NULL,
    CreatedAt TEXT NOT NULL,
    Resolved INTEGER NOT NULL DEFAULT 0
);

-- Sample data
INSERT INTO Collections (Location, CollectedAt, MaterialType, WeightKg) VALUES
('Center A', datetime('now','-2 days'), 'Plastic', 12.5),
('Center B', datetime('now','-1 day'), 'Paper', 8.2),
('Center C', datetime('now'), 'Metal', 5.0);

INSERT INTO Alerts (Message, CreatedAt, Resolved) VALUES
('Container near capacity at Center A', datetime('now','-1 hour'), 0);
