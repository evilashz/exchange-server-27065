using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200008C RID: 140
	public interface IReplidGuidMap
	{
		// Token: 0x060004F9 RID: 1273
		Guid GetGuidFromReplid(Context context, ushort replid);

		// Token: 0x060004FA RID: 1274
		ushort GetReplidFromGuid(Context context, Guid guid);

		// Token: 0x060004FB RID: 1275
		Guid InternalGetGuidFromReplid(Context context, ushort replid);
	}
}
