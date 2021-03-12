using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B9 RID: 1465
	[Serializable]
	public class RecipientStatisticsReport : ConfigurableObject
	{
		// Token: 0x0600335A RID: 13146 RVA: 0x000D0D99 File Offset: 0x000CEF99
		public RecipientStatisticsReport() : base(new RecipientStatisticsReportPropertyBag())
		{
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000D0DA6 File Offset: 0x000CEFA6
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return RecipientStatisticsReport.schema;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000D0DAD File Offset: 0x000CEFAD
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x000D0DC4 File Offset: 0x000CEFC4
		public new ADObjectId Identity
		{
			get
			{
				return (ADObjectId)this.propertyBag[RecipientStatisticsReportSchema.Identity];
			}
			internal set
			{
				this.propertyBag[RecipientStatisticsReportSchema.Identity] = value;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000D0DD7 File Offset: 0x000CEFD7
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000D0DDA File Offset: 0x000CEFDA
		// (set) Token: 0x06003360 RID: 13152 RVA: 0x000D0DF1 File Offset: 0x000CEFF1
		public uint TotalNumberOfMailboxes
		{
			get
			{
				return (uint)this.propertyBag[RecipientStatisticsReportSchema.TotalNumberOfMailboxes];
			}
			internal set
			{
				this.propertyBag[RecipientStatisticsReportSchema.TotalNumberOfMailboxes] = value;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000D0E09 File Offset: 0x000CF009
		// (set) Token: 0x06003362 RID: 13154 RVA: 0x000D0E20 File Offset: 0x000CF020
		public uint TotalNumberOfActiveMailboxes
		{
			get
			{
				return (uint)this.propertyBag[RecipientStatisticsReportSchema.TotalNumberOfActiveMailboxes];
			}
			internal set
			{
				this.propertyBag[RecipientStatisticsReportSchema.TotalNumberOfActiveMailboxes] = value;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000D0E38 File Offset: 0x000CF038
		// (set) Token: 0x06003364 RID: 13156 RVA: 0x000D0E4F File Offset: 0x000CF04F
		public uint NumberOfContacts
		{
			get
			{
				return (uint)this.propertyBag[RecipientStatisticsReportSchema.NumberOfContacts];
			}
			internal set
			{
				this.propertyBag[RecipientStatisticsReportSchema.NumberOfContacts] = value;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x000D0E67 File Offset: 0x000CF067
		// (set) Token: 0x06003366 RID: 13158 RVA: 0x000D0E7E File Offset: 0x000CF07E
		public uint NumberOfDistributionLists
		{
			get
			{
				return (uint)this.propertyBag[RecipientStatisticsReportSchema.NumberOfDistributionLists];
			}
			internal set
			{
				this.propertyBag[RecipientStatisticsReportSchema.NumberOfDistributionLists] = value;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x000D0E96 File Offset: 0x000CF096
		// (set) Token: 0x06003368 RID: 13160 RVA: 0x000D0EAD File Offset: 0x000CF0AD
		public DateTime LastUpdated
		{
			get
			{
				return (DateTime)this.propertyBag[RecipientStatisticsReportSchema.LastUpdated];
			}
			internal set
			{
				this.propertyBag[RecipientStatisticsReportSchema.LastUpdated] = value;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000D0EC8 File Offset: 0x000CF0C8
		public string LastUpdatedFormatted
		{
			get
			{
				return this.LastUpdated.ToShortDateString();
			}
		}

		// Token: 0x040023CF RID: 9167
		private static RecipientStatisticsReportSchema schema = ObjectSchema.GetInstance<RecipientStatisticsReportSchema>();
	}
}
