SELECT SUM(IntNumber) AS SumOfIntegers FROM Lines;

SELECT * 
FROM Lines 
WHERE DoubleNumber = 
(
    SELECT AVG(DoubleNumber)
    FROM (
        SELECT DoubleNumber
        FROM Lines
        ORDER BY DoubleNumber
        LIMIT 1 OFFSET (SELECT (COUNT(*) - 1) / 2 FROM Lines)
    )
);
