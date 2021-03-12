using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200036D RID: 877
	internal sealed class SuccessfulSetTransportResult : RopResult
	{
		// Token: 0x06001570 RID: 5488 RVA: 0x000378BE File Offset: 0x00035ABE
		internal SuccessfulSetTransportResult(StoreId transportQueueFolderId) : base(RopId.SetTransport, ErrorCode.None, null)
		{
			this.transportQueueFolderId = transportQueueFolderId;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000378D1 File Offset: 0x00035AD1
		internal SuccessfulSetTransportResult(Reader reader) : base(reader)
		{
			this.transportQueueFolderId = StoreId.Parse(reader);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000378E6 File Offset: 0x00035AE6
		internal static SuccessfulSetTransportResult Parse(Reader reader)
		{
			return new SuccessfulSetTransportResult(reader);
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x000378EE File Offset: 0x00035AEE
		public StoreId TransportQueueFolderId
		{
			get
			{
				return this.transportQueueFolderId;
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x000378F6 File Offset: 0x00035AF6
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.transportQueueFolderId.Serialize(writer);
		}

		// Token: 0x04000B35 RID: 2869
		private StoreId transportQueueFolderId;
	}
}
