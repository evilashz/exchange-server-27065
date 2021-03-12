using System;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x0200000F RID: 15
	public enum EasFolderType
	{
		// Token: 0x04000089 RID: 137
		UserGeneric = 1,
		// Token: 0x0400008A RID: 138
		Inbox,
		// Token: 0x0400008B RID: 139
		Drafts,
		// Token: 0x0400008C RID: 140
		DeletedItems,
		// Token: 0x0400008D RID: 141
		SentItems,
		// Token: 0x0400008E RID: 142
		Outbox,
		// Token: 0x0400008F RID: 143
		Tasks,
		// Token: 0x04000090 RID: 144
		Calendar,
		// Token: 0x04000091 RID: 145
		Contacts,
		// Token: 0x04000092 RID: 146
		Notes,
		// Token: 0x04000093 RID: 147
		Journal,
		// Token: 0x04000094 RID: 148
		UserMail,
		// Token: 0x04000095 RID: 149
		UserCalendar,
		// Token: 0x04000096 RID: 150
		UserContacts,
		// Token: 0x04000097 RID: 151
		UserTasks,
		// Token: 0x04000098 RID: 152
		UserJournal,
		// Token: 0x04000099 RID: 153
		UserNotes,
		// Token: 0x0400009A RID: 154
		Unknown,
		// Token: 0x0400009B RID: 155
		RecipientInfo,
		// Token: 0x0400009C RID: 156
		JunkEmail,
		// Token: 0x0400009D RID: 157
		Chats,
		// Token: 0x0400009E RID: 158
		SyntheticRoot = 200,
		// Token: 0x0400009F RID: 159
		SyntheticIpmSubtree,
		// Token: 0x040000A0 RID: 160
		OutOfRange = 255
	}
}
