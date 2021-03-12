using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B6 RID: 1462
	[Serializable]
	public class PhysicalAvailabilityReport : ConfigurableObject
	{
		// Token: 0x0600333B RID: 13115 RVA: 0x000D09C4 File Offset: 0x000CEBC4
		public PhysicalAvailabilityReport() : base(new PhysicalAvailabilityReportPropertyBag())
		{
			this.dailyStatistics = new MultiValuedProperty<DailyAvailability>();
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000D09DC File Offset: 0x000CEBDC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return PhysicalAvailabilityReport.schema;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000D09E3 File Offset: 0x000CEBE3
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000D09E6 File Offset: 0x000CEBE6
		// (set) Token: 0x0600333F RID: 13119 RVA: 0x000D09EE File Offset: 0x000CEBEE
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

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000D09F7 File Offset: 0x000CEBF7
		// (set) Token: 0x06003341 RID: 13121 RVA: 0x000D0A0E File Offset: 0x000CEC0E
		public string SiteName
		{
			get
			{
				return (string)this.propertyBag[PhysicalAvailabilityReportSchema.SiteName];
			}
			internal set
			{
				this.propertyBag[PhysicalAvailabilityReportSchema.SiteName] = value;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000D0A21 File Offset: 0x000CEC21
		// (set) Token: 0x06003343 RID: 13123 RVA: 0x000D0A38 File Offset: 0x000CEC38
		public DateTime StartDate
		{
			get
			{
				return (DateTime)this.propertyBag[PhysicalAvailabilityReportSchema.StartDate];
			}
			internal set
			{
				this.propertyBag[PhysicalAvailabilityReportSchema.StartDate] = value;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000D0A50 File Offset: 0x000CEC50
		public string StartDateFormatted
		{
			get
			{
				return this.StartDate.ToShortDateString();
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000D0A6B File Offset: 0x000CEC6B
		// (set) Token: 0x06003346 RID: 13126 RVA: 0x000D0A82 File Offset: 0x000CEC82
		public DateTime EndDate
		{
			get
			{
				return (DateTime)this.propertyBag[PhysicalAvailabilityReportSchema.EndDate];
			}
			internal set
			{
				this.propertyBag[PhysicalAvailabilityReportSchema.EndDate] = value;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000D0A9C File Offset: 0x000CEC9C
		public string EndDateFormatted
		{
			get
			{
				return this.EndDate.ToShortDateString();
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000D0AB7 File Offset: 0x000CECB7
		// (set) Token: 0x06003349 RID: 13129 RVA: 0x000D0ACE File Offset: 0x000CECCE
		public double AvailabilityPercentage
		{
			get
			{
				return (double)this.propertyBag[PhysicalAvailabilityReportSchema.AvailabilityPercentage];
			}
			internal set
			{
				this.propertyBag[PhysicalAvailabilityReportSchema.AvailabilityPercentage] = value;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000D0AE8 File Offset: 0x000CECE8
		public string AvailabilityPercentageFormatted
		{
			get
			{
				return this.AvailabilityPercentage.ToString("P2");
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000D0B08 File Offset: 0x000CED08
		// (set) Token: 0x0600334C RID: 13132 RVA: 0x000D0B1F File Offset: 0x000CED1F
		public double RawAvailabilityPercentage
		{
			get
			{
				return (double)this.propertyBag[PhysicalAvailabilityReportSchema.RawAvailabilityPercentage];
			}
			internal set
			{
				this.propertyBag[PhysicalAvailabilityReportSchema.RawAvailabilityPercentage] = value;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000D0B38 File Offset: 0x000CED38
		public string RawAvailabilityPercentageFormatted
		{
			get
			{
				return this.RawAvailabilityPercentage.ToString("P2");
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000D0B58 File Offset: 0x000CED58
		// (set) Token: 0x0600334F RID: 13135 RVA: 0x000D0B6F File Offset: 0x000CED6F
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this.propertyBag[PhysicalAvailabilityReportSchema.Database];
			}
			internal set
			{
				this.propertyBag[PhysicalAvailabilityReportSchema.Database] = value;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000D0B82 File Offset: 0x000CED82
		// (set) Token: 0x06003351 RID: 13137 RVA: 0x000D0B99 File Offset: 0x000CED99
		public ADObjectId ExchangeServer
		{
			get
			{
				return (ADObjectId)this.propertyBag[PhysicalAvailabilityReportSchema.ExchangeServer];
			}
			internal set
			{
				this.propertyBag[PhysicalAvailabilityReportSchema.ExchangeServer] = value;
			}
		}

		// Token: 0x040023C5 RID: 9157
		private static PhysicalAvailabilityReportSchema schema = ObjectSchema.GetInstance<PhysicalAvailabilityReportSchema>();

		// Token: 0x040023C6 RID: 9158
		private MultiValuedProperty<DailyAvailability> dailyStatistics;
	}
}
