using System;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001D0 RID: 464
	internal enum IMAPCommandType
	{
		// Token: 0x04000774 RID: 1908
		None,
		// Token: 0x04000775 RID: 1909
		Login,
		// Token: 0x04000776 RID: 1910
		Logout,
		// Token: 0x04000777 RID: 1911
		Select,
		// Token: 0x04000778 RID: 1912
		Fetch,
		// Token: 0x04000779 RID: 1913
		Append,
		// Token: 0x0400077A RID: 1914
		Noop,
		// Token: 0x0400077B RID: 1915
		Search,
		// Token: 0x0400077C RID: 1916
		List,
		// Token: 0x0400077D RID: 1917
		CreateMailbox,
		// Token: 0x0400077E RID: 1918
		DeleteMailbox,
		// Token: 0x0400077F RID: 1919
		RenameMailbox,
		// Token: 0x04000780 RID: 1920
		Capability,
		// Token: 0x04000781 RID: 1921
		Store,
		// Token: 0x04000782 RID: 1922
		Expunge,
		// Token: 0x04000783 RID: 1923
		Id,
		// Token: 0x04000784 RID: 1924
		Starttls,
		// Token: 0x04000785 RID: 1925
		Authenticate,
		// Token: 0x04000786 RID: 1926
		Status
	}
}
