using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002EF RID: 751
	internal sealed class RopLockRegionStream : RopLockUnlockRegionStreamBase
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x000305E4 File Offset: 0x0002E7E4
		internal override RopId RopId
		{
			get
			{
				return RopId.LockRegionStream;
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000305E8 File Offset: 0x0002E7E8
		internal static Rop CreateRop()
		{
			return new RopLockRegionStream();
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000305EF File Offset: 0x0002E7EF
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopLockRegionStream.resultFactory;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000305F6 File Offset: 0x0002E7F6
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.LockRegionStream(serverObject, this.offset, this.regionLength, this.lockType, RopLockRegionStream.resultFactory);
		}

		// Token: 0x0400094B RID: 2379
		private const RopId RopType = RopId.LockRegionStream;

		// Token: 0x0400094C RID: 2380
		private static LockRegionStreamResultFactory resultFactory = new LockRegionStreamResultFactory();
	}
}
