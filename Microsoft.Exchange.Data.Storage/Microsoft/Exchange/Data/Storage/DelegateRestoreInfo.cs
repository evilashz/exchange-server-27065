using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct DelegateRestoreInfo
	{
		// Token: 0x0400019B RID: 411
		public IList<IExchangePrincipal> Principals;

		// Token: 0x0400019C RID: 412
		public string[] Names;

		// Token: 0x0400019D RID: 413
		public byte[][] Ids;

		// Token: 0x0400019E RID: 414
		public int[] Flags;

		// Token: 0x0400019F RID: 415
		public int[] Flags2;

		// Token: 0x040001A0 RID: 416
		public bool BossWantsCopy;

		// Token: 0x040001A1 RID: 417
		public bool BossWantsInfo;

		// Token: 0x040001A2 RID: 418
		public bool DontMailDelegate;

		// Token: 0x040001A3 RID: 419
		public IDictionary<StoreObjectId, IDictionary<ADRecipient, MemberRights>> FolderPermissions;

		// Token: 0x040001A4 RID: 420
		public IList<ADObjectId> SendOnBehalf;

		// Token: 0x040001A5 RID: 421
		public Rule DelegateRule;
	}
}
