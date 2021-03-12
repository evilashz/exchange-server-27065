using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x0200008B RID: 139
	internal class MsgTrackExpandInfo
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0001476F File Offset: 0x0001296F
		public MsgTrackExpandInfo(RoutingAddress groupAddress, long? relatedMailItemId, string statusText) : this(null, groupAddress, relatedMailItemId, statusText)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001477B File Offset: 0x0001297B
		public MsgTrackExpandInfo(string sourceContext, RoutingAddress groupAddress, long? relatedMailItemId, string statusText)
		{
			this.sourceContext = sourceContext;
			this.groupAddress = groupAddress;
			this.relatedMailItemId = relatedMailItemId;
			this.statusText = statusText;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x000147A0 File Offset: 0x000129A0
		internal string SourceContext
		{
			get
			{
				return this.sourceContext;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000147A8 File Offset: 0x000129A8
		internal RoutingAddress GroupAddress
		{
			get
			{
				return this.groupAddress;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000147B0 File Offset: 0x000129B0
		internal long? RelatedMailItemId
		{
			get
			{
				return this.relatedMailItemId;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000147B8 File Offset: 0x000129B8
		internal string StatusText
		{
			get
			{
				return this.statusText;
			}
		}

		// Token: 0x04000262 RID: 610
		private string sourceContext;

		// Token: 0x04000263 RID: 611
		private RoutingAddress groupAddress;

		// Token: 0x04000264 RID: 612
		private long? relatedMailItemId;

		// Token: 0x04000265 RID: 613
		private string statusText;
	}
}
