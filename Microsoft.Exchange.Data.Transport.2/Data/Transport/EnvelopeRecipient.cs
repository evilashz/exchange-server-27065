using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200006A RID: 106
	public abstract class EnvelopeRecipient
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00006856 File Offset: 0x00004A56
		internal EnvelopeRecipient()
		{
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600021E RID: 542
		// (set) Token: 0x0600021F RID: 543
		public abstract RoutingAddress Address { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000220 RID: 544
		[Obsolete("Use ResolvedMessageEventSource.GetRoutingOverride() instead")]
		public abstract RoutingDomain RoutingOverride { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000221 RID: 545
		// (set) Token: 0x06000222 RID: 546
		public abstract string OriginalRecipient { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000223 RID: 547
		// (set) Token: 0x06000224 RID: 548
		public abstract DsnTypeRequested RequestedReports { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000225 RID: 549
		public abstract IDictionary<string, object> Properties { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000226 RID: 550
		public abstract DeliveryMethod OutboundDeliveryMethod { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000227 RID: 551
		public abstract RecipientCategory RecipientCategory { get; }

		// Token: 0x06000228 RID: 552
		[Obsolete("Use ResolvedMessageEventSource.SetRoutingOverride() instead")]
		public abstract void SetRoutingOverride(RoutingDomain routingDomain);

		// Token: 0x06000229 RID: 553 RVA: 0x0000685E File Offset: 0x00004A5E
		internal virtual bool IsPublicFolderRecipient()
		{
			return false;
		}
	}
}
