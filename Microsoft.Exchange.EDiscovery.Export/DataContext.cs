using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000032 RID: 50
	internal class DataContext
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00005E08 File Offset: 0x00004008
		public DataContext(SourceInformation sourceInformation, IItemIdList itemIdList)
		{
			this.sourceInformation = sourceInformation;
			this.ItemIdList = itemIdList;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00005E1E File Offset: 0x0000401E
		public SourceInformation SourceInformation
		{
			get
			{
				return this.sourceInformation;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005E26 File Offset: 0x00004026
		public string SourceId
		{
			get
			{
				return this.sourceInformation.Configuration.Id;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00005E38 File Offset: 0x00004038
		public string SourceName
		{
			get
			{
				return this.sourceInformation.Configuration.Name;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005E4A File Offset: 0x0000404A
		public string SourceLegacyExchangeDN
		{
			get
			{
				return this.sourceInformation.Configuration.LegacyExchangeDN;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005E5C File Offset: 0x0000405C
		public ISourceDataProvider ServiceClient
		{
			get
			{
				return this.sourceInformation.ServiceClient;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005E69 File Offset: 0x00004069
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00005E71 File Offset: 0x00004071
		public IItemIdList ItemIdList { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005E7A File Offset: 0x0000407A
		public bool IsUnsearchable
		{
			get
			{
				return this.ItemIdList != null && this.ItemIdList.IsUnsearchable;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005E91 File Offset: 0x00004091
		public bool IsPublicFolder
		{
			get
			{
				return this.SourceId.StartsWith("\\");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005EA8 File Offset: 0x000040A8
		public int ItemCount
		{
			get
			{
				if (!this.IsUnsearchable)
				{
					return this.sourceInformation.Status.ItemCount;
				}
				return this.sourceInformation.Status.UnsearchableItemCount;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005ED3 File Offset: 0x000040D3
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005EFE File Offset: 0x000040FE
		public int ProcessedItemCount
		{
			get
			{
				if (!this.IsUnsearchable)
				{
					return this.sourceInformation.Status.ProcessedItemCount;
				}
				return this.sourceInformation.Status.ProcessedUnsearchableItemCount;
			}
			set
			{
				if (this.IsUnsearchable)
				{
					this.sourceInformation.Status.ProcessedUnsearchableItemCount = value;
					return;
				}
				this.sourceInformation.Status.ProcessedItemCount = value;
			}
		}

		// Token: 0x040000A6 RID: 166
		private SourceInformation sourceInformation;
	}
}
