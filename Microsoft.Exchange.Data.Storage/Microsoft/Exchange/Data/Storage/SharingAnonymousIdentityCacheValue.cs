using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DC0 RID: 3520
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SharingAnonymousIdentityCacheValue
	{
		// Token: 0x060078E3 RID: 30947 RVA: 0x0021603F File Offset: 0x0021423F
		public SharingAnonymousIdentityCacheValue(string folderId, SecurityIdentifier sid)
		{
			this.folderId = folderId;
			this.sid = sid;
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x060078E4 RID: 30948 RVA: 0x00216055 File Offset: 0x00214255
		public string FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x060078E5 RID: 30949 RVA: 0x0021605D File Offset: 0x0021425D
		public SecurityIdentifier Sid
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x060078E6 RID: 30950 RVA: 0x00216065 File Offset: 0x00214265
		public bool IsAccessAllowed
		{
			get
			{
				return !string.IsNullOrEmpty(this.folderId);
			}
		}

		// Token: 0x04005381 RID: 21377
		private readonly string folderId;

		// Token: 0x04005382 RID: 21378
		private readonly SecurityIdentifier sid;
	}
}
