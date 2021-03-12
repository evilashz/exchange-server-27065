using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000299 RID: 665
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderContentMailboxInfo
	{
		// Token: 0x06001B8A RID: 7050 RVA: 0x0007F6D4 File Offset: 0x0007D8D4
		public PublicFolderContentMailboxInfo(string contentMailboxInfo)
		{
			this.isValid = (GuidHelper.TryParseGuid(contentMailboxInfo, out this.mailboxGuid) && this.mailboxGuid != Guid.Empty);
			this.contentMailboxInfo = contentMailboxInfo;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0007F70A File Offset: 0x0007D90A
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0007F712 File Offset: 0x0007D912
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0007F71A File Offset: 0x0007D91A
		public override string ToString()
		{
			return this.contentMailboxInfo;
		}

		// Token: 0x04001322 RID: 4898
		private readonly Guid mailboxGuid;

		// Token: 0x04001323 RID: 4899
		private readonly bool isValid;

		// Token: 0x04001324 RID: 4900
		private readonly string contentMailboxInfo;
	}
}
