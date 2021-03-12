using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000841 RID: 2113
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ManifestItemReadUnread : ManifestChangeBase
	{
		// Token: 0x06004E6D RID: 20077 RVA: 0x0014885E File Offset: 0x00146A5E
		internal ManifestItemReadUnread(byte[] idsetReadUnread, bool isRead)
		{
			this.idsetReadUnread = idsetReadUnread;
			this.isRead = isRead;
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x00148874 File Offset: 0x00146A74
		public byte[] IdsetReadUnread
		{
			get
			{
				return this.idsetReadUnread;
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06004E6F RID: 20079 RVA: 0x0014887C File Offset: 0x00146A7C
		public bool IsRead
		{
			get
			{
				return this.isRead;
			}
		}

		// Token: 0x04002ACA RID: 10954
		private readonly byte[] idsetReadUnread;

		// Token: 0x04002ACB RID: 10955
		private readonly bool isRead;
	}
}
