SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMessages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblMessages](
	[MessageID] [bigint] IDENTITY(1,1) NOT NULL,
	[ConversationID] [bigint] NOT NULL,
	[MessageData] [image] NOT NULL,
 CONSTRAINT [PK_tblMessages] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblConversationMembers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblConversationMembers](
	[ConversationMemberID] [bigint] IDENTITY(1,1) NOT NULL,
	[ConversationID] [bigint] NOT NULL,
	[UserID] [varchar](36) NOT NULL,
 CONSTRAINT [PK_tblConversationMembers] PRIMARY KEY CLUSTERED 
(
	[ConversationMemberID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMissedConversations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblMissedConversations](
	[MissedMessageID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](36) NOT NULL,
	[ConversatioID] [bigint] NOT NULL,
 CONSTRAINT [PK_tblMissedConversations] PRIMARY KEY CLUSTERED 
(
	[MissedMessageID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblFiles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblFiles](
	[FileItemID] [bigint] IDENTITY(1,1) NOT NULL,
	[FileID] [varchar](36) NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[UploadDate] [datetime] NOT NULL,
	[FileBytes] [image] NOT NULL,
	[FileHash] [varchar](255) NOT NULL,
 CONSTRAINT [PK_tblFiles] PRIMARY KEY CLUSTERED 
(
	[FileItemID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLastKnownStates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLastKnownStates](
	[StateID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](36) NOT NULL,
	[LastKnownState] [varchar](255) NOT NULL,
 CONSTRAINT [PK_tblLastKnownStates] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblConversationList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblConversationList](
	[ConversationID] [bigint] IDENTITY(1,1) NOT NULL,
	[ConversationGUID] [varchar](36) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblConversationList] PRIMARY KEY CLUSTERED 
(
	[ConversationID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
