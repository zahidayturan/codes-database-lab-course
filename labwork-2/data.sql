USE NHL_DB;

INSERT INTO Stadiums (StadiumID, Name, Location, Capacity) VALUES
(1, 'Madison Square Garden', 'New York, NY', 18006),
(2, 'TD Garden', 'Boston, MA', 17565),
(3, 'Amerant Bank Arena', 'Sunrise, FL', 19250),
(4, 'Rogers Place', 'Edmonton, AB, Canada', 18347),
(5, 'Delta Center', 'Salt Lake City, UT', 14900),
(6, 'Wrigley Field', 'Chicago, IL', 41000),
(7, 'Bell Centre', 'Montreal, QC, Canada', 21105);


INSERT INTO Teams (TeamID, Name, StadiumID, City) VALUES
(1, 'New York Rangers', 1, 'New York'),
(2, 'Boston Bruins', 2, 'Boston'),
(3, 'Florida Panthers', 3, 'Sunrise'),
(4, 'Edmonton Oilers', 4, 'Edmonton'),
(5, 'Utah Hockey Club', 5, 'Salt Lake City'),
(6, 'Montreal Canadiens', 6, 'Montreal');


INSERT INTO Players (PlayerID, TeamID, Name, BirthDate, StartYear, JerseyNumber) VALUES
(1, 1, 'Chris Kreider', '1991-04-30', 2012, 20),
(2, 1, 'Mika Zibanejad', '1993-04-18', 2016, 93),
(3, 1, 'Ryan Lindgren', '1997-10-19', 2018, 55),
(4, 1, 'Jacob Trouba', '1994-02-20', 2019, 8),
(5, 1, 'Adam Fox', '1998-01-17', 2019, 23),
(6, 1, 'KAndre Miller', '2000-01-21', 2020, 79),

(7, 2, 'David Pastrnak', '1996-05-25', 2014, 88),
(8, 2, 'Brad Marchand', '1988-05-11', 2009, 63),
(9, 2, 'Patrice Bergeron', '1985-07-24', 2003, 37),
(10, 2, 'Charlie McAvoy', '1997-12-21', 2017, 73),
(11, 2, 'Taylor Hall', '1992-11-14', 2011, 71),
(12, 2, 'Jake DeBrusk', '1996-10-17', 2015, 74),

(13, 3, 'Alexander Barkov', '1995-09-02', 2013, 16),
(14, 3, 'Jonathan Huberdeau', '1993-06-04', 2012, 11),
(15, 3, 'Aaron Ekblad', '1996-02-07', 2014, 5),
(16, 3, 'Sam Bennett', '1996-06-23', 2016, 9),
(17, 3, 'Anthony Duclair', '1996-08-26', 2014, 10),
(18, 3, 'Spencer Knight', '2001-04-19', 2021, 30),

(19, 4, 'Connor McDavid', '1997-01-13', 2015, 97),
(20, 4, 'Leon Draisaitl', '1996-10-27', 2014, 29),
(21, 4, 'Ryan Nugent-Hopkins', '1993-04-12', 2011, 93),
(22, 4, 'Zack Kassian', '1991-01-24', 2009, 44),
(23, 4, 'Darnell Nurse', '1995-02-09', 2015, 25),
(24, 4, 'Evander Kane', '1991-08-02', 2009, 91),

(25, 5, 'Jonah Gadjovich', '1999-11-06', 2020, 22),
(26, 5, 'Brad Malone', '1989-04-01', 2011, 12),
(27, 5, 'Gordie Dwyer', '1990-05-22', 2012, 39),
(28, 5, 'Michael Duco', '1989-11-18', 2011, 18),
(29, 5, 'Tyler Gaudet', '1993-04-17', 2014, 47),
(30, 5, 'Matthew Boucher', '1992-07-29', 2013, 25),

(31, 6, 'Cole Caufield', '2001-01-02', 2020, 22),
(32, 6, 'Nick Suzuki', '1999-08-10', 2019, 14),
(33, 6, 'Jake Evans', '1996-05-02', 2020, 71),
(34, 6, 'Josh Anderson', '1994-01-07', 2016, 17),
(35, 6, 'Brendan Gallagher', '1992-05-06', 2010, 11),
(36, 6, 'Jeff Petry', '1987-12-09', 2010, 26);



INSERT INTO Matches (MatchID, HomeTeamID, AwayTeamID, StadiumID, MatchDate, Result) VALUES
(1, 1, 2, 1, '2024-10-08', '3-1'),
(2, 3, 4, 3, '2024-12-31', '2-6'),
(3, 5, 1, 5, '2025-01-15', '3-2 OT'),
(4, 4, 2, 4, '2025-01-20', '2-0'),
(5, 6, 5, 2, '2025-01-25', '4-2');


INSERT INTO MatchParticipation (MatchID, PlayerID, GoalsScored, YellowCard, RedCard) VALUES
(1, 5, 1, 1, 0),
(1, 6, 2, 0, 0),
(1, 4, 0, 1, 0),
(1, 9, 1, 1, 0),

(2, 17, 1, 1, 0),
(2, 16, 0, 0, 1),
(2, 20, 1, 0, 0),
(2, 21, 3, 0, 0),
(2, 24, 2, 1, 0),

(3, 4, 2, 1, 0),
(3, 5, 1, 0, 0),
(3, 30, 2, 1, 0),

(4, 23, 2, 2, 0),
(4, 9, 0, 1, 0),

(5, 34, 3, 0, 0),
(5, 36, 1, 0, 0),
(5, 31, 0, 1, 0),
(5, 29, 2, 0, 0);


INSERT INTO Substitutions (MatchID, PlayerInID, PlayerOutID, SubstitutionTime) VALUES
(1, 6, 5, '00:35:00'),
(1, 9, 10, '00:40:00'),

(2, 14, 15, '00:42:00'),
(2, 20, 23, '00:45:00'),

(3, 4, 6, '00:50:00'),
(3, 25, 26, '00:55:00'),

(4, 19, 24, '00:15:00'),
(4, 7, 6, '00:40:00'),

(5, 27, 28, '00:45:00'),
(5, 34, 34, '00:35:00');


INSERT INTO Referees (RefereeID, Name, BirthDate, ExperienceYears) VALUES
(1, 'Wes McCauley', '1970-01-11', 21), --h
(2, 'Chris Rooney', '1975-05-12', 18), -- h
(3, 'Cody Beach', '1992-08-08', 5), -- h
(4, 'Kelly Sutherland', '1971-04-18', 24), -- r
(5, 'Dan O\Rourke', '1973-11-26', 18), -- r
(6, 'Steve Kozari', '1971-06-29', 20), -- r
(7, 'Pierre Racicot', '1975-03-30', 17); --- r


INSERT INTO MatchReferees (MatchID, RefereeID, Role) VALUES
(1, 1, 'Head Referee'),
(1, 4, 'Linesman'),
(1, 5, 'Linesman'),

(2, 2, 'Head Referee'),
(2, 6, 'Linesman'),
(2, 7, 'Linesman'),

(3, 3, 'Head Referee'),
(3, 4, 'Linesman'),
(3, 7, 'Linesman'),

(4, 1, 'Head Referee'),
(4, 5, 'Linesman'),
(4, 6, 'Linesman'),

(5, 2, 'Head Referee'),
(5, 5, 'Linesman'),
(5, 7, 'Linesman');