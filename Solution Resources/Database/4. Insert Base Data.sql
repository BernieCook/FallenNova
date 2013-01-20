INSERT INTO Corporation 
	(CorporationId, Name) 
	VALUES 
	(1, 'Fallen Nova')

INSERT INTO UserStatus
	(UserStatusId, Title)
	VALUES
	(1, 'Active')
INSERT INTO UserStatus
	(UserStatusId, Title)
	VALUES
	(2, 'Disabled')
INSERT INTO UserStatus
	(UserStatusId, Title)
	VALUES
	(3, 'Deleted')
	
INSERT INTO [Role]
	(RoleId, Title)
	VALUES
	(1, 'Member')
INSERT INTO [Role]
	(RoleId, Title)
	VALUES
	(2, 'Administrator')
	
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(1, 'Logged In Successfully, Manual Login')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(2, 'Logged Out')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(3, 'Added User')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(4, 'Edited User Details')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(5, 'Edited User Status')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(6, 'Submitted Contact Us Message')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(7, 'Logged In Unsuccessfully, Manual Login')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(8, 'Authenticated Successfully, Automatic Login')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(9, 'Updated Eve Online Skill Tree')
INSERT INTO UserLogType
	(UserLogTypeId, Title)
	VALUES
	(10, 'Edited User Password')

-- The password prior to being hashed is: password123
INSERT INTO [User]
	(UserStatusId, FirstName, Surname, EmailAddress, HashedPassword, UnsuccessfulLoginAttempts, LastSuccessfulAuthenticationDateTime, LastSuccessfulLoginDateTime, AddedByUserId, AddedDateTime, ModifiedByUserId, ModifiedDateTime)
	VALUES
    (1, 'Bernie', 'Cook', 'berniecook@fallennova.com', '$2a$12$0TRnr7snkT9u7bKrNBWzL.Wr478uncnyPCXtuIWCcQ7C0iGmt9NBO', 0, GETDATE(), GETDATE(), 1, GETDATE(), 1, GETDATE())

INSERT INTO [UserRole]
	(UserId, RoleId)
	VALUES
    (1, 2)

INSERT INTO [UserLog]
	(UserId, UserLogTypeId, ActionAgainstUserId, AddedDateTime)
	VALUES
    (1, 3, 1, GETDATE())
