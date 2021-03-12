using System;
using System.Collections.Generic;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200004E RID: 78
	public interface IFolderInformation
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000752 RID: 1874
		ExchangeShortId Fid { get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000753 RID: 1875
		ExchangeShortId ParentFid { get; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000754 RID: 1876
		int MailboxNumber { get; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000755 RID: 1877
		bool IsSearchFolder { get; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000756 RID: 1878
		string DisplayName { get; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000757 RID: 1879
		bool IsPartOfContentIndexing { get; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000758 RID: 1880
		bool IsInternalAccess { get; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000759 RID: 1881
		long MessageCount { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600075A RID: 1882
		SecurityDescriptor SecurityDescriptor { get; }

		// Token: 0x0600075B RID: 1883
		IEnumerable<ExchangeShortId> AllDescendantFolderIds();
	}
}
