
SELECT
    u.Email,
    (SELECT COUNT(*)
     FROM dbo.CartItems AS c
     WHERE c.UserId = u.Id) AS ProduseInCos,
    (SELECT COUNT(*)
     FROM dbo.Favorites AS f
     WHERE f.UserId = u.Id) AS Favorite
FROM dbo.AspNetUsers AS u
ORDER BY u.Email;
