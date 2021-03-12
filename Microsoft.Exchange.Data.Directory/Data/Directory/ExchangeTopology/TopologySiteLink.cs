using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x020006BE RID: 1726
	internal sealed class TopologySiteLink : ADSiteLink, ITopologySiteLink
	{
		// Token: 0x06004FB5 RID: 20405 RVA: 0x001265A3 File Offset: 0x001247A3
		internal TopologySiteLink(ADSiteLink siteLink)
		{
			this.propertyBag = siteLink.propertyBag;
			this.SetIsReadOnly(siteLink.IsReadOnly);
			this.m_Session = siteLink.Session;
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06004FB6 RID: 20406 RVA: 0x001265CF File Offset: 0x001247CF
		// (set) Token: 0x06004FB7 RID: 20407 RVA: 0x001265D7 File Offset: 0x001247D7
		public ReadOnlyCollection<ITopologySite> TopologySites
		{
			get
			{
				return this.topologySites;
			}
			internal set
			{
				this.topologySites = value;
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x001265E0 File Offset: 0x001247E0
		ulong ITopologySiteLink.AbsoluteMaxMessageSize
		{
			get
			{
				Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)this[ADSiteLinkSchema.MaxMessageSize];
				if (!unlimited.IsUnlimited)
				{
					return unlimited.Value.ToBytes();
				}
				return ADSiteLink.UnlimitedMaxMessageSize;
			}
		}

		// Token: 0x0400366A RID: 13930
		private ReadOnlyCollection<ITopologySite> topologySites;
	}
}
