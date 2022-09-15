CREATE TABLE dbo.Cars(
  	ID INT NOT NULL IDENTITY(1,1),
  	Model VARCHAR(50) NOT NULL,
  	LicensePlate VARCHAR(50) NOT NULL
PRIMARY KEY (ID))
GO

 CREATE PROCEDURE dbo.AddCar
  @model VARCHAR(50), 
  @licensePlane VARCHAR(50),
	@id INT OUTPUT
AS 
BEGIN 
  SET NOCOUNT ON 

  INSERT INTO dbo.Cars(Model,LicensePlate) 
  VALUES (@model,@licensePlane)
  SET @id=SCOPE_IDENTITY()
  RETURN  @id
END 
GO

CREATE PROCEDURE dbo.UpdateCar
  @id INT,
  @model VARCHAR(50), 
  @licensePlane VARCHAR(50),
  @count INT OUTPUT
AS 
BEGIN 
 SET NOCOUNT ON 

   UPDATE dbo.Cars SET
	 Model=@model, 
	 LicensePlate=@licensePlane
	 WHERE ID=@id
	 SET @count=@@ROWCOUNT
   RETURN @count
END
GO
  
  
CREATE PROCEDURE dbo.GetCount
	   @count INT OUTPUT
AS 
BEGIN 
   SET NOCOUNT ON 
   select @count =count(*) from dbo.Cars
	 RETURN
END
GO
  
CREATE PROCEDURE dbo.DeleteCar
	 @id INT,
	 @count INT OUTPUT
AS 
BEGIN 
	SET NOCOUNT ON 

	DELETE FROM dbo.Cars WHERE ID=@id
	SET @count = @@ROWCOUNT
	RETURN
END
GO
  
CREATE PROCEDURE dbo.GetSingleCar 
	 @id INT
AS 
BEGIN 
   SET NOCOUNT ON 

   SELECT * FROM dbo.Cars WHERE ID=@id
   RETURN
END 
GO

CREATE PROCEDURE dbo.GetCars
	 @skip INT,
	 @limit INT
AS 
BEGIN 
   SET NOCOUNT ON 

   SELECT * FROM dbo.Cars
	 ORDER BY ID 
	 OFFSET @skip ROWS
	 FETCH NEXT @limit ROWS ONLY;
END 
GO