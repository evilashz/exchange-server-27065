using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003DB RID: 987
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class StructuredError
	{
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x00077848 File Offset: 0x00075A48
		// (set) Token: 0x06001F9C RID: 8092 RVA: 0x00077850 File Offset: 0x00075A50
		[DataMember]
		public string Message { get; set; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x00077859 File Offset: 0x00075A59
		// (set) Token: 0x06001F9E RID: 8094 RVA: 0x00077861 File Offset: 0x00075A61
		[DataMember]
		public string Info { get; set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x0007786A File Offset: 0x00075A6A
		// (set) Token: 0x06001FA0 RID: 8096 RVA: 0x00077872 File Offset: 0x00075A72
		[DataMember]
		public string TargetSite { get; set; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x0007787B File Offset: 0x00075A7B
		// (set) Token: 0x06001FA2 RID: 8098 RVA: 0x00077883 File Offset: 0x00075A83
		[DataMember]
		public string Source { get; set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0007788C File Offset: 0x00075A8C
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x00077894 File Offset: 0x00075A94
		[DataMember]
		public int Code { get; set; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0007789D File Offset: 0x00075A9D
		// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x000778A5 File Offset: 0x00075AA5
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x000778AE File Offset: 0x00075AAE
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x000778B6 File Offset: 0x00075AB6
		[DataMember]
		public string Guid { get; set; }
	}
}
