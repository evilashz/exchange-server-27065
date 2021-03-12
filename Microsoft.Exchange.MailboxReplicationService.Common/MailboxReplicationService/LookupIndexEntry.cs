using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000148 RID: 328
	internal class LookupIndexEntry<TData> where TData : class
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x00015865 File Offset: 0x00013A65
		public LookupIndexEntry()
		{
			this.dataList = new List<TData>();
			this.isResolved = false;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00015880 File Offset: 0x00013A80
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x000158B1 File Offset: 0x00013AB1
		public TData Data
		{
			get
			{
				if (this.dataList.Count == 1)
				{
					return this.dataList[0];
				}
				return default(TData);
			}
			set
			{
				if (!this.dataList.Contains(value))
				{
					this.dataList.Add(value);
				}
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000158CD File Offset: 0x00013ACD
		public List<TData> DataList
		{
			get
			{
				return this.dataList;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000158D5 File Offset: 0x00013AD5
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x000158DD File Offset: 0x00013ADD
		public bool IsResolved
		{
			get
			{
				return this.isResolved;
			}
			set
			{
				this.isResolved = value;
			}
		}

		// Token: 0x04000659 RID: 1625
		private List<TData> dataList;

		// Token: 0x0400065A RID: 1626
		private bool isResolved;
	}
}
