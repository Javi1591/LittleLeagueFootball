-- Stored Procedure to get all players by team ID
-- Creates GetPlayersByTeam to return players for a specific team

USE LittleLeagueFootball;
GO

-- Drop the procedure if it already exists
If OBJECT_ID('dbo.GetPlayersByTeam', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.GetPlayersByTeam;
END
GO

-- Create the stored procedure
--  Use @TeamID as input parameter to filter players by team
CREATE PROCEDURE dbo.GetPlayersByTeam
	@TeamID INT
AS
BEGIN
	SET NOCOUNT ON;			-- Prevent extra result sets

	-- Select all players where TeamID matches the input parameter
	SELECT
		p.PlayerID,
		p.FirstName,
		p.LastName,
		t.TeamID,
		t.TeamName AS TeamName
	From dbo.Players AS p

	-- Join with Teams table to get team details
	INNER JOIN dbo.Teams AS t

	-- Join condition on TeamID
		ON p.TeamID = t.TeamID

	-- Filter players by the provided TeamID
	WHERE p.TeamID = @TeamID

	-- Order the results by LastName and FirstName
	ORDER BY p.LastName, p.FirstName;
	END
	GO