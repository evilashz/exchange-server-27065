using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200035C RID: 860
	internal sealed class RopUnlockRegionStream : RopLockUnlockRegionStreamBase
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0003684A File Offset: 0x00034A4A
		internal override RopId RopId
		{
			get
			{
				return RopId.UnlockRegionStream;
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0003684E File Offset: 0x00034A4E
		internal static Rop CreateRop()
		{
			return new RopUnlockRegionStream();
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00036855 File Offset: 0x00034A55
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopUnlockRegionStream.resultFactory;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0003685C File Offset: 0x00034A5C
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.UnlockRegionStream(serverObject, this.offset, this.regionLength, this.lockType, RopUnlockRegionStream.resultFactory);
		}

		// Token: 0x04000B09 RID: 2825
		private const RopId RopType = RopId.UnlockRegionStream;

		// Token: 0x04000B0A RID: 2826
		private static UnlockRegionStreamResultFactory resultFactory = new UnlockRegionStreamResultFactory();
	}
}
