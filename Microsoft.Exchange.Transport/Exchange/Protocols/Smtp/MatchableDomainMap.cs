using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003FA RID: 1018
	internal class MatchableDomainMap<T> : IEnumerable<KeyValuePair<MatchableDomain, T>>, IEnumerable
	{
		// Token: 0x06002E91 RID: 11921 RVA: 0x000BBCA3 File Offset: 0x000B9EA3
		public MatchableDomainMap()
		{
			this.list = new List<KeyValuePair<MatchableDomain, T>>();
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000BBCB6 File Offset: 0x000B9EB6
		public MatchableDomainMap(int capacity)
		{
			this.list = new List<KeyValuePair<MatchableDomain, T>>(capacity);
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000BBCCA File Offset: 0x000B9ECA
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000BBCD8 File Offset: 0x000B9ED8
		public void Add(MatchableDomain domain, T value)
		{
			ArgumentValidator.ThrowIfNull("domain", domain);
			KeyValuePair<MatchableDomain, T> item = new KeyValuePair<MatchableDomain, T>(domain, value);
			int num = this.list.BinarySearch(item, MatchableDomainMap<T>.MatchableDomainComparer.Comparer);
			if (num < 0)
			{
				num = ~num;
			}
			this.list.Insert(num, item);
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000BBD1F File Offset: 0x000B9F1F
		public IEnumerator<KeyValuePair<MatchableDomain, T>> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000BBD31 File Offset: 0x000B9F31
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x040016F8 RID: 5880
		private readonly List<KeyValuePair<MatchableDomain, T>> list;

		// Token: 0x020003FB RID: 1019
		private class MatchableDomainComparer : IComparer<KeyValuePair<MatchableDomain, T>>
		{
			// Token: 0x17000E29 RID: 3625
			// (get) Token: 0x06002E97 RID: 11927 RVA: 0x000BBD43 File Offset: 0x000B9F43
			public static MatchableDomainMap<T>.MatchableDomainComparer Comparer
			{
				get
				{
					return MatchableDomainMap<T>.MatchableDomainComparer.comparer;
				}
			}

			// Token: 0x06002E98 RID: 11928 RVA: 0x000BBD4C File Offset: 0x000B9F4C
			public int Compare(KeyValuePair<MatchableDomain, T> x, KeyValuePair<MatchableDomain, T> y)
			{
				if (x.Key.Domain.IncludeSubDomains == y.Key.Domain.IncludeSubDomains)
				{
					return y.Key.DotCount - x.Key.DotCount;
				}
				if (!x.Key.Domain.IncludeSubDomains)
				{
					return -1;
				}
				return 1;
			}

			// Token: 0x040016F9 RID: 5881
			private static MatchableDomainMap<T>.MatchableDomainComparer comparer = new MatchableDomainMap<T>.MatchableDomainComparer();
		}
	}
}
