using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	public abstract class HttpReply
	{
		// Token: 0x02000073 RID: 115
		[Serializable]
		public sealed class ExceptionReply : HttpReply
		{
			// Token: 0x060004C4 RID: 1220 RVA: 0x00010908 File Offset: 0x0000EB08
			public ExceptionReply(Exception e)
			{
				this.Exception = e;
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00010917 File Offset: 0x0000EB17
			// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0001091F File Offset: 0x0000EB1F
			public Exception Exception { get; private set; }
		}

		// Token: 0x02000074 RID: 116
		[Serializable]
		public sealed class DxStoreReply : HttpReply
		{
			// Token: 0x060004C7 RID: 1223 RVA: 0x00010928 File Offset: 0x0000EB28
			public DxStoreReply(DxStoreReplyBase r)
			{
				this.Reply = r;
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00010937 File Offset: 0x0000EB37
			// (set) Token: 0x060004C9 RID: 1225 RVA: 0x0001093F File Offset: 0x0000EB3F
			public DxStoreReplyBase Reply { get; set; }
		}

		// Token: 0x02000075 RID: 117
		[Serializable]
		public sealed class GetInstanceStatusReply : HttpReply
		{
			// Token: 0x060004CA RID: 1226 RVA: 0x00010948 File Offset: 0x0000EB48
			public GetInstanceStatusReply(InstanceStatusInfo r)
			{
				this.Reply = r;
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060004CB RID: 1227 RVA: 0x00010957 File Offset: 0x0000EB57
			// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001095F File Offset: 0x0000EB5F
			public InstanceStatusInfo Reply { get; set; }
		}
	}
}
