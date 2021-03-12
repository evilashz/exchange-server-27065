using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000118 RID: 280
	[DataContract]
	[Serializable]
	public sealed class UnifiedPolicyStatus
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x00017733 File Offset: 0x00015933
		public UnifiedPolicyStatus()
		{
			this.Version = PolicyVersion.Empty;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00017746 File Offset: 0x00015946
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0001774E File Offset: 0x0001594E
		[DataMember]
		public Guid TenantId { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00017757 File Offset: 0x00015957
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001775F File Offset: 0x0001595F
		[DataMember]
		public ConfigurationObjectType ObjectType { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00017768 File Offset: 0x00015968
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00017770 File Offset: 0x00015970
		[DataMember]
		public Guid ObjectId { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00017779 File Offset: 0x00015979
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00017781 File Offset: 0x00015981
		[DataMember]
		public Guid? ParentObjectId { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001778A File Offset: 0x0001598A
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00017792 File Offset: 0x00015992
		[DataMember]
		public PolicyVersion Version { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001779B File Offset: 0x0001599B
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x000177A3 File Offset: 0x000159A3
		[DataMember]
		public Workload Workload { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000177AC File Offset: 0x000159AC
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x000177B4 File Offset: 0x000159B4
		[DataMember]
		public UnifiedPolicyErrorCode ErrorCode { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x000177BD File Offset: 0x000159BD
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x000177C5 File Offset: 0x000159C5
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x000177CE File Offset: 0x000159CE
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x000177D6 File Offset: 0x000159D6
		[DataMember]
		public DateTime WhenProcessedUTC { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x000177DF File Offset: 0x000159DF
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x000177E7 File Offset: 0x000159E7
		[DataMember]
		public Mode Mode { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000177F0 File Offset: 0x000159F0
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x000177F8 File Offset: 0x000159F8
		[DataMember]
		public string AdditionalDiagnostics { get; set; }

		// Token: 0x060007CA RID: 1994 RVA: 0x00017804 File Offset: 0x00015A04
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("TenantId:{0},", this.TenantId));
			stringBuilder.Append(string.Format("ObjectType:{0},", this.ObjectType));
			StringBuilder stringBuilder2 = stringBuilder;
			string format = "ObjectId:{0},";
			Guid objectId = this.ObjectId;
			stringBuilder2.Append(string.Format(format, this.ObjectId.ToString()));
			stringBuilder.Append(string.Format("ParentObjectId:{0},", this.ParentObjectId));
			stringBuilder.Append(string.Format("Version:{0},", (this.Version == null) ? "<null>" : this.Version.ToString()));
			stringBuilder.Append(string.Format("Workload:{0},", this.Workload));
			stringBuilder.Append(string.Format("ErrorCode:{0},", this.ErrorCode));
			stringBuilder.Append(string.Format("ErrorMessage:{0},", this.ErrorMessage));
			stringBuilder.Append(string.Format("WhenProcessedUTC:{0},", this.WhenProcessedUTC));
			stringBuilder.Append(string.Format("Mode:{0},", this.Mode));
			stringBuilder.Append(string.Format("AdditionalDiagnostics:{0}", this.AdditionalDiagnostics));
			return stringBuilder.ToString();
		}
	}
}
