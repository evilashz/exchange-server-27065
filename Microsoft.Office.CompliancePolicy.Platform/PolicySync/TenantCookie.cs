using System;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000116 RID: 278
	[DataContract]
	public sealed class TenantCookie
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x00017612 File Offset: 0x00015812
		public TenantCookie(Guid tenantId, byte[] cookie, Workload workload, ConfigurationObjectType objectType, DateTime? deletedObjectTimeThreshold)
		{
			this.TenantId = tenantId;
			this.Cookie = cookie;
			this.Workload = workload;
			this.ObjectType = objectType;
			this.DeletedObjectTimeThreshold = deletedObjectTimeThreshold;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001763F File Offset: 0x0001583F
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x00017647 File Offset: 0x00015847
		[DataMember]
		public Guid TenantId { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00017650 File Offset: 0x00015850
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x00017658 File Offset: 0x00015858
		[DataMember]
		public bool MoreData { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00017661 File Offset: 0x00015861
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00017669 File Offset: 0x00015869
		[DataMember]
		public byte[] Cookie { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00017672 File Offset: 0x00015872
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0001767A File Offset: 0x0001587A
		[DataMember]
		public Workload Workload { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00017683 File Offset: 0x00015883
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0001768B File Offset: 0x0001588B
		[DataMember]
		public ConfigurationObjectType ObjectType { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00017694 File Offset: 0x00015894
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0001769C File Offset: 0x0001589C
		[DataMember]
		public DateTime? DeletedObjectTimeThreshold { get; private set; }
	}
}
