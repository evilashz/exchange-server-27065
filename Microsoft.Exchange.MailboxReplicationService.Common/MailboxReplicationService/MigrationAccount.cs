using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004F RID: 79
	[DataContract(Name = "AccountToMigrate")]
	internal class MigrationAccount
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000077E8 File Offset: 0x000059E8
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x000077F0 File Offset: 0x000059F0
		[DataMember(Name = "clusterName")]
		public string ClusterName { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x000077F9 File Offset: 0x000059F9
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00007801 File Offset: 0x00005A01
		[DataMember(Name = "dgroupId")]
		public uint DgroupId { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000780A File Offset: 0x00005A0A
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00007812 File Offset: 0x00005A12
		[DataMember(Name = "puid")]
		public long Puid { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000781B File Offset: 0x00005A1B
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00007823 File Offset: 0x00005A23
		[DataMember(Name = "login")]
		public string Login { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000782C File Offset: 0x00005A2C
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x00007834 File Offset: 0x00005A34
		[DataMember(Name = "firstName")]
		public string FirstName { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000783D File Offset: 0x00005A3D
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00007845 File Offset: 0x00005A45
		[DataMember(Name = "lastName")]
		public string LastName { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000784E File Offset: 0x00005A4E
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00007856 File Offset: 0x00005A56
		[DataMember(Name = "accountSize")]
		public long AccountSize { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000785F File Offset: 0x00005A5F
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00007867 File Offset: 0x00005A67
		[DataMember(Name = "timeZone")]
		public string TimeZone { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00007870 File Offset: 0x00005A70
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00007878 File Offset: 0x00005A78
		[DataMember(Name = "lcid")]
		public string Lcid { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00007881 File Offset: 0x00005A81
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00007889 File Offset: 0x00005A89
		[DataMember(Name = "leaseInitialExpiry")]
		public DateTime LeaseInitialExpiry { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00007892 File Offset: 0x00005A92
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000789A File Offset: 0x00005A9A
		[DataMember(Name = "aliases")]
		public string[] Aliases { get; set; }
	}
}
