using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001D8 RID: 472
	internal class ReroutableItem : RestrictedItem
	{
		// Token: 0x06001558 RID: 5464 RVA: 0x00055E4B File Offset: 0x0005404B
		public ReroutableItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00055E54 File Offset: 0x00054054
		public ADObjectId HomeMtaServerId
		{
			get
			{
				return base.GetProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.HomeMtaServerId");
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00055E64 File Offset: 0x00054064
		public string HomeMTA
		{
			get
			{
				ADObjectId homeMtaServerId = this.HomeMtaServerId;
				if (homeMtaServerId != null)
				{
					return homeMtaServerId.DistinguishedName;
				}
				return null;
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00055E84 File Offset: 0x00054084
		public override void Allow(Expansion expansion)
		{
			string homeMTA = this.HomeMTA;
			if (string.IsNullOrEmpty(homeMTA) || homeMTA.Equals(ResolverConfiguration.ServerDN, StringComparison.OrdinalIgnoreCase))
			{
				this.ProcessLocally(expansion);
				return;
			}
			this.ProcessRemotely();
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00055EBC File Offset: 0x000540BC
		public override void AddItemVisited(Expansion expansion)
		{
			ExTraceGlobals.ResolverTracer.TraceDebug<RoutingAddress>(0L, "Adding group '{0}' to the visited list", base.Email);
			expansion.Resolver.ResolverCache.AddToResolvedRecipientCache(base.ObjectGuid);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00055EEB File Offset: 0x000540EB
		protected virtual void ProcessLocally(Expansion expansion)
		{
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00055EED File Offset: 0x000540ED
		protected virtual void ProcessRemotely()
		{
		}
	}
}
