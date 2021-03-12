using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005BE RID: 1470
	[Serializable]
	public class ServiceAvailabilityReport : ConfigurableObject
	{
		// Token: 0x06003389 RID: 13193 RVA: 0x000D16AC File Offset: 0x000CF8AC
		public ServiceAvailabilityReport() : base(new ServiceAvailabilityReportPropertyBag())
		{
			this.dailyStatistics = new MultiValuedProperty<DailyAvailability>();
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000D16C4 File Offset: 0x000CF8C4
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ServiceAvailabilityReport.schema;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000D16CB File Offset: 0x000CF8CB
		// (set) Token: 0x0600338C RID: 13196 RVA: 0x000D16E2 File Offset: 0x000CF8E2
		public new ObjectId Identity
		{
			get
			{
				return (ObjectId)this.propertyBag[ServiceAvailabilityReportSchema.Identity];
			}
			internal set
			{
				this.propertyBag[ServiceAvailabilityReportSchema.Identity] = value;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x0600338D RID: 13197 RVA: 0x000D16F8 File Offset: 0x000CF8F8
		public string IdentityFormatted
		{
			get
			{
				if (this.Identity != null)
				{
					ADObjectId adobjectId = (ADObjectId)this.Identity;
					return adobjectId.Name;
				}
				return null;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000D1721 File Offset: 0x000CF921
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x0600338F RID: 13199 RVA: 0x000D1724 File Offset: 0x000CF924
		// (set) Token: 0x06003390 RID: 13200 RVA: 0x000D172C File Offset: 0x000CF92C
		public MultiValuedProperty<DailyAvailability> DailyStatistics
		{
			get
			{
				return this.dailyStatistics;
			}
			internal set
			{
				this.dailyStatistics = value;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x000D1735 File Offset: 0x000CF935
		// (set) Token: 0x06003392 RID: 13202 RVA: 0x000D174C File Offset: 0x000CF94C
		public DateTime StartDate
		{
			get
			{
				return (DateTime)this.propertyBag[ServiceAvailabilityReportSchema.StartDate];
			}
			internal set
			{
				this.propertyBag[ServiceAvailabilityReportSchema.StartDate] = value;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x000D1764 File Offset: 0x000CF964
		public string StartDateFormatted
		{
			get
			{
				return this.StartDate.ToShortDateString();
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000D177F File Offset: 0x000CF97F
		// (set) Token: 0x06003395 RID: 13205 RVA: 0x000D1796 File Offset: 0x000CF996
		public DateTime EndDate
		{
			get
			{
				return (DateTime)this.propertyBag[ServiceAvailabilityReportSchema.EndDate];
			}
			internal set
			{
				this.propertyBag[ServiceAvailabilityReportSchema.EndDate] = value;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000D17B0 File Offset: 0x000CF9B0
		public string EndDateFormatted
		{
			get
			{
				return this.EndDate.ToShortDateString();
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x000D17CB File Offset: 0x000CF9CB
		// (set) Token: 0x06003398 RID: 13208 RVA: 0x000D17E2 File Offset: 0x000CF9E2
		public double AvailabilityPercentage
		{
			get
			{
				return (double)this.propertyBag[ServiceAvailabilityReportSchema.AvailabilityPercentage];
			}
			internal set
			{
				this.propertyBag[ServiceAvailabilityReportSchema.AvailabilityPercentage] = value;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000D17FC File Offset: 0x000CF9FC
		public string AvailabilityPercentageFormatted
		{
			get
			{
				return this.AvailabilityPercentage.ToString("P2");
			}
		}

		// Token: 0x040023DC RID: 9180
		private static ServiceAvailabilityReportSchema schema = ObjectSchema.GetInstance<ServiceAvailabilityReportSchema>();

		// Token: 0x040023DD RID: 9181
		private MultiValuedProperty<DailyAvailability> dailyStatistics;
	}
}
