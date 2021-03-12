using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x0200008F RID: 143
	internal class MsgTrackResolveInfo
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00014CB2 File Offset: 0x00012EB2
		public MsgTrackResolveInfo(string sourceContext, RoutingAddress origAddress, RoutingAddress newAddress)
		{
			this.sourceContext = sourceContext;
			this.originalAddress = origAddress;
			this.resolvedAddress = newAddress;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00014CCF File Offset: 0x00012ECF
		internal string SourceContext
		{
			get
			{
				return this.sourceContext;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00014CD7 File Offset: 0x00012ED7
		internal RoutingAddress OriginalAddress
		{
			get
			{
				return this.originalAddress;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00014CDF File Offset: 0x00012EDF
		internal RoutingAddress ResolvedAddress
		{
			get
			{
				return this.resolvedAddress;
			}
		}

		// Token: 0x04000282 RID: 642
		private string sourceContext;

		// Token: 0x04000283 RID: 643
		private RoutingAddress originalAddress;

		// Token: 0x04000284 RID: 644
		private RoutingAddress resolvedAddress;
	}
}
