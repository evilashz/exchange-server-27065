using System;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000102 RID: 258
	internal class RoutedQueueBase : DataRow
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x00026A38 File Offset: 0x00024C38
		internal RoutedQueueBase(long id, NextHopSolutionKey key) : base(Components.MessagingDatabase.Database.QueueTable)
		{
			this.Id = id;
			this.NextHopConnector = key.NextHopConnector;
			this.NextHopType = key.NextHopType;
			this.SetNextHopDomainAndTlsDomain(key.NextHopDomain, key.NextHopTlsDomain);
			this.State = 0;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00026A96 File Offset: 0x00024C96
		private RoutedQueueBase() : base(Components.MessagingDatabase.Database.QueueTable)
		{
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00026AAD File Offset: 0x00024CAD
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00026AC5 File Offset: 0x00024CC5
		public long Id
		{
			get
			{
				return ((ColumnCache<long>)base.Columns[0]).Value;
			}
			private set
			{
				((ColumnCache<long>)base.Columns[0]).Value = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00026ADE File Offset: 0x00024CDE
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00026AF6 File Offset: 0x00024CF6
		public Guid NextHopConnector
		{
			get
			{
				return ((ColumnCache<Guid>)base.Columns[1]).Value;
			}
			private set
			{
				((ColumnCache<Guid>)base.Columns[1]).Value = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00026B10 File Offset: 0x00024D10
		public string NextHopDomain
		{
			get
			{
				string result;
				string text;
				this.SeperateDomainAndTls(((ColumnCache<string>)base.Columns[2]).Value, out result, out text);
				return result;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00026B40 File Offset: 0x00024D40
		public string NextHopTlsDomain
		{
			get
			{
				string text;
				string result;
				this.SeperateDomainAndTls(((ColumnCache<string>)base.Columns[2]).Value, out text, out result);
				return result;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00026B70 File Offset: 0x00024D70
		private void SetNextHopDomainAndTlsDomain(string nextHopDomain, string tlsDomain)
		{
			string value = string.Format("{0}{1}{2}", nextHopDomain, "/", tlsDomain);
			((ColumnCache<string>)base.Columns[2]).Value = value;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00026BA8 File Offset: 0x00024DA8
		private void SeperateDomainAndTls(string storedValue, out string nextHopDomain, out string nextHopTlsDomain)
		{
			if (string.IsNullOrEmpty(storedValue))
			{
				nextHopDomain = string.Empty;
				nextHopTlsDomain = string.Empty;
				return;
			}
			int num = storedValue.IndexOf("/");
			if (num < 0)
			{
				nextHopDomain = storedValue;
				nextHopTlsDomain = string.Empty;
				return;
			}
			nextHopDomain = storedValue.Substring(0, num);
			nextHopTlsDomain = storedValue.Substring(num + 1);
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00026BFC File Offset: 0x00024DFC
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00026C19 File Offset: 0x00024E19
		public NextHopType NextHopType
		{
			get
			{
				return new NextHopType(((ColumnCache<int>)base.Columns[4]).Value);
			}
			private set
			{
				((ColumnCache<int>)base.Columns[4]).Value = value.ToInt32();
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00026C38 File Offset: 0x00024E38
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x00026C48 File Offset: 0x00024E48
		public bool Suspended
		{
			get
			{
				return (this.State & 1) != 0;
			}
			set
			{
				this.State = ((value ? 1 : 0) | (this.State & -2));
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00026C61 File Offset: 0x00024E61
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x00026C7C File Offset: 0x00024E7C
		private int State
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[3]).Value;
			}
			set
			{
				ColumnCache<int> columnCache = (ColumnCache<int>)base.Columns[3];
				if (!columnCache.HasValue || columnCache.Value != value)
				{
					((ColumnCache<int>)base.Columns[3]).Value = value;
				}
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00026CC4 File Offset: 0x00024EC4
		public static RoutedQueueBase LoadFromRow(DataTableCursor cursor)
		{
			RoutedQueueBase routedQueueBase = new RoutedQueueBase();
			routedQueueBase.LoadFromCurrentRow(cursor);
			return routedQueueBase;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00026CDF File Offset: 0x00024EDF
		public new void Commit()
		{
			base.Commit(TransactionCommitMode.Lazy);
		}

		// Token: 0x040004CA RID: 1226
		private const string NextHopDomainAndTlsDomainSeperator = "/";

		// Token: 0x02000103 RID: 259
		private enum StateBits
		{
			// Token: 0x040004CC RID: 1228
			Active,
			// Token: 0x040004CD RID: 1229
			Suspended
		}
	}
}
