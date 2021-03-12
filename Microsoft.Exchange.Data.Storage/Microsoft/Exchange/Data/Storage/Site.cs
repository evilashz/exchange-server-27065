using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D71 RID: 3441
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Site : IComparable<Site>, IEquatable<Site>
	{
		// Token: 0x060076CF RID: 30415 RVA: 0x0020D30A File Offset: 0x0020B50A
		public Site(TopologySite topologySite) : this(topologySite, new ServiceTopology.All(null))
		{
		}

		// Token: 0x060076D0 RID: 30416 RVA: 0x0020D31C File Offset: 0x0020B51C
		internal Site(TopologySite topologySite, ServiceTopology.All all)
		{
			this.DistinguishedName = topologySite.DistinguishedName;
			all.Sites.Add(this.DistinguishedName, this);
			this.Id = topologySite.Id;
			this.Guid = topologySite.Guid;
			this.Name = topologySite.Name;
			this.PartnerId = topologySite.PartnerId;
			this.MinorPartnerId = topologySite.MinorPartnerId;
			List<ADObjectId> list = new List<ADObjectId>(topologySite.ResponsibleForSites);
			this.ResponsibleForSites = list.AsReadOnly();
			if (topologySite.TopologySiteLinks == null || topologySite.TopologySiteLinks.Count == 0)
			{
				this.SiteLinks = SiteLink.EmptyCollection;
				return;
			}
			List<SiteLink> list2 = new List<SiteLink>(topologySite.TopologySiteLinks.Count);
			foreach (ITopologySiteLink topologySiteLink in topologySite.TopologySiteLinks)
			{
				TopologySiteLink topologySiteLink2 = (TopologySiteLink)topologySiteLink;
				SiteLink item = SiteLink.Get(topologySiteLink2, all);
				list2.Add(item);
			}
			this.SiteLinks = list2.AsReadOnly();
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x060076D1 RID: 30417 RVA: 0x0020D434 File Offset: 0x0020B634
		// (set) Token: 0x060076D2 RID: 30418 RVA: 0x0020D43C File Offset: 0x0020B63C
		public string DistinguishedName { get; private set; }

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x060076D3 RID: 30419 RVA: 0x0020D445 File Offset: 0x0020B645
		// (set) Token: 0x060076D4 RID: 30420 RVA: 0x0020D44D File Offset: 0x0020B64D
		public ICollection<ADObjectId> ResponsibleForSites { get; private set; }

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x060076D5 RID: 30421 RVA: 0x0020D456 File Offset: 0x0020B656
		// (set) Token: 0x060076D6 RID: 30422 RVA: 0x0020D45E File Offset: 0x0020B65E
		public ADObjectId Id { get; private set; }

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x060076D7 RID: 30423 RVA: 0x0020D467 File Offset: 0x0020B667
		// (set) Token: 0x060076D8 RID: 30424 RVA: 0x0020D46F File Offset: 0x0020B66F
		public string Name { get; private set; }

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x060076D9 RID: 30425 RVA: 0x0020D478 File Offset: 0x0020B678
		// (set) Token: 0x060076DA RID: 30426 RVA: 0x0020D480 File Offset: 0x0020B680
		public Guid Guid { get; private set; }

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x060076DB RID: 30427 RVA: 0x0020D489 File Offset: 0x0020B689
		// (set) Token: 0x060076DC RID: 30428 RVA: 0x0020D491 File Offset: 0x0020B691
		public int PartnerId { get; private set; }

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x060076DD RID: 30429 RVA: 0x0020D49A File Offset: 0x0020B69A
		// (set) Token: 0x060076DE RID: 30430 RVA: 0x0020D4A2 File Offset: 0x0020B6A2
		public int MinorPartnerId { get; private set; }

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x060076DF RID: 30431 RVA: 0x0020D4AB File Offset: 0x0020B6AB
		// (set) Token: 0x060076E0 RID: 30432 RVA: 0x0020D4B3 File Offset: 0x0020B6B3
		public ReadOnlyCollection<SiteLink> SiteLinks { get; internal set; }

		// Token: 0x060076E1 RID: 30433 RVA: 0x0020D4BC File Offset: 0x0020B6BC
		public bool Equals(Site other)
		{
			return other != null && (object.ReferenceEquals(this, other) || this.DistinguishedName.Equals(other.DistinguishedName));
		}

		// Token: 0x060076E2 RID: 30434 RVA: 0x0020D4DF File Offset: 0x0020B6DF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Site);
		}

		// Token: 0x060076E3 RID: 30435 RVA: 0x0020D4ED File Offset: 0x0020B6ED
		public override int GetHashCode()
		{
			return this.DistinguishedName.GetHashCode();
		}

		// Token: 0x060076E4 RID: 30436 RVA: 0x0020D4FA File Offset: 0x0020B6FA
		public override string ToString()
		{
			return this.DistinguishedName;
		}

		// Token: 0x060076E5 RID: 30437 RVA: 0x0020D504 File Offset: 0x0020B704
		public int CompareTo(Site site)
		{
			if (site == null)
			{
				throw new ArgumentException();
			}
			int num = this.GetHashCode() - site.GetHashCode();
			if (num == 0)
			{
				num = this.DistinguishedName.CompareTo(site.DistinguishedName);
			}
			return num;
		}

		// Token: 0x060076E6 RID: 30438 RVA: 0x0020D540 File Offset: 0x0020B740
		internal static Site Get(TopologySite topologySite, ServiceTopology.All all)
		{
			Site result;
			if (!all.Sites.TryGetValue(topologySite.DistinguishedName, out result))
			{
				result = new Site(topologySite, all);
			}
			return result;
		}

		// Token: 0x04005264 RID: 21092
		internal static readonly ReadOnlyCollection<Site> EmptyCollection = new List<Site>().AsReadOnly();
	}
}
