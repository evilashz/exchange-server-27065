using System;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000005 RID: 5
	internal enum ImapCommandType
	{
		// Token: 0x0400002A RID: 42
		None,
		// Token: 0x0400002B RID: 43
		Login,
		// Token: 0x0400002C RID: 44
		Logout,
		// Token: 0x0400002D RID: 45
		Select,
		// Token: 0x0400002E RID: 46
		Fetch,
		// Token: 0x0400002F RID: 47
		Append,
		// Token: 0x04000030 RID: 48
		Noop,
		// Token: 0x04000031 RID: 49
		Search,
		// Token: 0x04000032 RID: 50
		List,
		// Token: 0x04000033 RID: 51
		CreateMailbox,
		// Token: 0x04000034 RID: 52
		DeleteMailbox,
		// Token: 0x04000035 RID: 53
		RenameMailbox,
		// Token: 0x04000036 RID: 54
		Capability,
		// Token: 0x04000037 RID: 55
		Store,
		// Token: 0x04000038 RID: 56
		Expunge,
		// Token: 0x04000039 RID: 57
		Id,
		// Token: 0x0400003A RID: 58
		Starttls,
		// Token: 0x0400003B RID: 59
		Authenticate,
		// Token: 0x0400003C RID: 60
		Status
	}
}
