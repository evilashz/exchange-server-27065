using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F8 RID: 1784
	[Table(Name = "dbo.PartnerClientExpiringSubscription")]
	[DataServiceKey("Date")]
	[Serializable]
	public class PartnerClientExpiringSubscriptionReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06003ED8 RID: 16088 RVA: 0x001051F1 File Offset: 0x001033F1
		// (set) Token: 0x06003ED9 RID: 16089 RVA: 0x001051F9 File Offset: 0x001033F9
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x00105202 File Offset: 0x00103402
		// (set) Token: 0x06003EDB RID: 16091 RVA: 0x0010520A File Offset: 0x0010340A
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x00105213 File Offset: 0x00103413
		// (set) Token: 0x06003EDD RID: 16093 RVA: 0x0010521B File Offset: 0x0010341B
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x00105224 File Offset: 0x00103424
		// (set) Token: 0x06003EDF RID: 16095 RVA: 0x0010522C File Offset: 0x0010342C
		[Column(Name = "PartnerId")]
		public string PartnerId { get; set; }

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x00105235 File Offset: 0x00103435
		// (set) Token: 0x06003EE1 RID: 16097 RVA: 0x0010523D File Offset: 0x0010343D
		[Column(Name = "ManagedTenantGuid")]
		public Guid ManagedTenantGuid { get; set; }

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x00105246 File Offset: 0x00103446
		// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x0010524E File Offset: 0x0010344E
		[Column(Name = "ManagedTenantOrganizationName")]
		public string ManagedTenantOrganizationName { get; set; }

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x00105257 File Offset: 0x00103457
		// (set) Token: 0x06003EE5 RID: 16101 RVA: 0x0010525F File Offset: 0x0010345F
		[Column(Name = "OfferId")]
		public Guid OfferId { get; set; }

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x00105268 File Offset: 0x00103468
		// (set) Token: 0x06003EE7 RID: 16103 RVA: 0x00105270 File Offset: 0x00103470
		[Column(Name = "OfferName")]
		public string OfferName { get; set; }

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x00105279 File Offset: 0x00103479
		// (set) Token: 0x06003EE9 RID: 16105 RVA: 0x00105281 File Offset: 0x00103481
		[Column(Name = "IsOfferTrial")]
		public bool IsOfferTrial { get; set; }

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06003EEA RID: 16106 RVA: 0x0010528A File Offset: 0x0010348A
		// (set) Token: 0x06003EEB RID: 16107 RVA: 0x00105292 File Offset: 0x00103492
		[Column(Name = "SubscriptionID")]
		public Guid SubscriptionID { get; set; }

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x0010529B File Offset: 0x0010349B
		// (set) Token: 0x06003EED RID: 16109 RVA: 0x001052A3 File Offset: 0x001034A3
		[Column(Name = "SubscriptionEndDate")]
		public DateTime SubscriptionEndDate { get; set; }

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x001052AC File Offset: 0x001034AC
		// (set) Token: 0x06003EEF RID: 16111 RVA: 0x001052B4 File Offset: 0x001034B4
		[Column(Name = "IsAutoRenew")]
		public bool IsAutoRenew { get; set; }

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06003EF0 RID: 16112 RVA: 0x001052BD File Offset: 0x001034BD
		// (set) Token: 0x06003EF1 RID: 16113 RVA: 0x001052C5 File Offset: 0x001034C5
		[Column(Name = "LicenseQuantity")]
		public int? LicenseQuantity { get; set; }
	}
}
