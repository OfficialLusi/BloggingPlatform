CREATE TABLE Users ( 
	UserId			INTEGER PRIMARY KEY	AUTOINCREMENT	,             
	UserGuid		TEXT NOT NULL						,             
	UserName		TEXT NOT NULL						,             
	UserSurname		TEXT NOT NULL						,          
	UserEmail		TEXT NOT NULL						,            
	UserCreatedOn	TEXT NOT NULL						,
	Salt			TEXT NULL							,
	HashCode		TEXT NULL			
	);                                  


CREATE TABLE BlogPosts ( 
   PostId							INTEGER PRIMARY KEY	AUTOINCREMENT	,                
   UserId							INTEGER								,                            
   PostGuid							TEXT NOT NULL						,                
   PostTitle						TEXT NOT NULL						,               
   PostContent						TEXT NOT NULL						,             
   PostTags							TEXT NOT NULL						,             
   PostCreatedOn					TEXT NOT NULL						,           
   PostModifiedOn					TEXT NULL							,    
   FOREIGN KEY (UserId) REFERENCES Users(UserId) 
   );


INSERT INTO Users (UserGuid, UserName, UserSurname, UserEmail, UserCreatedOn) 
VALUES ('366166CC-37D8-4669-9057-4068DF6D4BC8', 'admin', 'admin@admin.admin', 'admin', '2025-01-01T00:00:00Z');