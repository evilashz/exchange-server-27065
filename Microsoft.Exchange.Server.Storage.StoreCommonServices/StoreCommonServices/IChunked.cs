using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200007E RID: 126
	public interface IChunked
	{
		// Token: 0x0600049A RID: 1178
		bool DoChunk(Context context);

		// Token: 0x0600049B RID: 1179
		void Dispose(Context context);

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600049C RID: 1180
		bool MustYield { get; }
	}
}
