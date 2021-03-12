using System;
using System.Collections.Generic;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000017 RID: 23
	internal interface IHierarchySynchronizationScope
	{
		// Token: 0x060000F1 RID: 241
		ExchangeId GetExchangeId(long shortTermId);

		// Token: 0x060000F2 RID: 242
		ReplId GuidToReplid(Guid guid);

		// Token: 0x060000F3 RID: 243
		Guid ReplidToGuid(ReplId replid);

		// Token: 0x060000F4 RID: 244
		IdSet GetServerCnsetSeen(MapiContext operationContext);

		// Token: 0x060000F5 RID: 245
		void GetChangedAndDeletedFolders(MapiContext operationContext, SyncFlag syncFlags, IdSet cnsetSeen, IdSet idsetGiven, out IList<FolderChangeEntry> changedFolders, out IdSet idsetNewDeletes);

		// Token: 0x060000F6 RID: 246
		ExchangeId GetRootFid();

		// Token: 0x060000F7 RID: 247
		MapiFolder OpenFolder(ExchangeId fid);
	}
}
