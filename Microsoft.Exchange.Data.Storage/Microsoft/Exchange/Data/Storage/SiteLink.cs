using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D72 RID: 3442
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SiteLink
	{
		// Token: 0x060076E8 RID: 30440 RVA: 0x0020D57C File Offset: 0x0020B77C
		internal SiteLink(TopologySiteLink topologySiteLink, ServiceTopology.All all)
		{
			this.DistinguishedName = topologySiteLink.DistinguishedName;
			all.SiteLinks.Add(this.DistinguishedName, this);
			this.Name = topologySiteLink.Name;
			this.Cost = topologySiteLink.Cost;
			if (topologySiteLink.TopologySites == null || topologySiteLink.TopologySites.Count == 0)
			{
				this.Sites = Site.EmptyCollection;
				return;
			}
			List<Site> list = new List<Site>(topologySiteLink.TopologySites.Count);
			foreach (ITopologySite topologySite in topologySiteLink.TopologySites)
			{
				TopologySite topologySite2 = (TopologySite)topologySite;
				Site item = Site.Get(topologySite2, all);
				list.Add(item);
			}
			this.Sites = list.AsReadOnly();
		}

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x060076E9 RID: 30441 RVA: 0x0020D658 File Offset: 0x0020B858
		// (set) Token: 0x060076EA RID: 30442 RVA: 0x0020D660 File Offset: 0x0020B860
		public string DistinguishedName { get; private set; }

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x060076EB RID: 30443 RVA: 0x0020D669 File Offset: 0x0020B869
		// (set) Token: 0x060076EC RID: 30444 RVA: 0x0020D671 File Offset: 0x0020B871
		public string Name { get; private set; }

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x060076ED RID: 30445 RVA: 0x0020D67A File Offset: 0x0020B87A
		// (set) Token: 0x060076EE RID: 30446 RVA: 0x0020D682 File Offset: 0x0020B882
		public int Cost { get; private set; }

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x060076EF RID: 30447 RVA: 0x0020D68B File Offset: 0x0020B88B
		// (set) Token: 0x060076F0 RID: 30448 RVA: 0x0020D693 File Offset: 0x0020B893
		public ReadOnlyCollection<Site> Sites { get; internal set; }

		// Token: 0x060076F1 RID: 30449 RVA: 0x0020D69C File Offset: 0x0020B89C
		internal static SiteLink Get(TopologySiteLink topologySiteLink, ServiceTopology.All all)
		{
			SiteLink result;
			if (!all.SiteLinks.TryGetValue(topologySiteLink.DistinguishedName, out result))
			{
				result = new SiteLink(topologySiteLink, all);
			}
			return result;
		}

		// Token: 0x0400526D RID: 21101
		internal static readonly ReadOnlyCollection<SiteLink> EmptyCollection = new List<SiteLink>().AsReadOnly();
	}
}
