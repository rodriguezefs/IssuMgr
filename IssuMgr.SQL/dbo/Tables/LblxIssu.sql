CREATE TABLE [dbo].[LblxIssu]
(
	[IssuId] INT NOT NULL , 
    [LblId] INT NOT NULL, 
    PRIMARY KEY ([LblId], [IssuId]), 
    CONSTRAINT [FK_LblxIssu_Issu] FOREIGN KEY ([IssuId]) REFERENCES [Issu]([IssuId]),
	CONSTRAINT [FK_LblxIssu_Lbl] FOREIGN KEY ([LblId]) REFERENCES [Lbl]([LblId])
)
