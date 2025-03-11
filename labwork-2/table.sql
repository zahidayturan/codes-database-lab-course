USE NHL_DB;

CREATE TABLE Stadiums (
    StadiumID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Location VARCHAR(100),
    Capacity INT
);

CREATE TABLE Teams (
    TeamID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    StadiumID INT,
    City VARCHAR(100),
    FOREIGN KEY (StadiumID) REFERENCES Stadiums(StadiumID) ON DELETE SET NULL
);

CREATE TABLE Players (
    PlayerID INT PRIMARY KEY,
    TeamID INT,
    Name VARCHAR(100) NOT NULL,
    BirthDate DATE,
    StartYear INT,
    JerseyNumber INT,
    FOREIGN KEY (TeamID) REFERENCES Teams(TeamID) ON DELETE SET NULL
);

CREATE TABLE Matches (
    MatchID INT PRIMARY KEY,
    HomeTeamID INT NOT NULL,
    AwayTeamID INT NOT NULL,
    StadiumID INT,
    MatchDate DATE NOT NULL,
    Result VARCHAR(50),
    FOREIGN KEY (HomeTeamID) REFERENCES Teams(TeamID) ON DELETE CASCADE,
    FOREIGN KEY (AwayTeamID) REFERENCES Teams(TeamID) ON DELETE NO ACTION,
    FOREIGN KEY (StadiumID) REFERENCES Stadiums(StadiumID) ON DELETE SET NULL
);

CREATE TABLE MatchParticipation (
    MatchID INT NOT NULL,
    PlayerID INT NOT NULL,
    GoalsScored INT DEFAULT 0,
    YellowCard INT DEFAULT 0,
    RedCard INT DEFAULT 0,
    PRIMARY KEY (MatchID, PlayerID),
    FOREIGN KEY (MatchID) REFERENCES Matches(MatchID) ON DELETE CASCADE,
    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID) ON DELETE CASCADE
);

CREATE TABLE Substitutions (
    MatchID INT NOT NULL,
    PlayerInID INT NOT NULL,
    PlayerOutID INT NOT NULL,
    SubstitutionTime TIME NOT NULL,
    PRIMARY KEY (MatchID, PlayerInID, PlayerOutID),
    FOREIGN KEY (MatchID) REFERENCES Matches(MatchID) ON DELETE CASCADE,
    FOREIGN KEY (PlayerInID) REFERENCES Players(PlayerID) ON DELETE NO ACTION,
    FOREIGN KEY (PlayerOutID) REFERENCES Players(PlayerID) ON DELETE NO ACTION
);

CREATE TABLE Referees (
    RefereeID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    BirthDate DATE,
    ExperienceYears INT
);

CREATE TABLE MatchReferees (
    MatchID INT NOT NULL,
    RefereeID INT NOT NULL,
    Role VARCHAR(50) NOT NULL,
    PRIMARY KEY (MatchID, RefereeID),
    FOREIGN KEY (MatchID) REFERENCES Matches(MatchID) ON DELETE CASCADE,
    FOREIGN KEY (RefereeID) REFERENCES Referees(RefereeID) ON DELETE CASCADE
);
