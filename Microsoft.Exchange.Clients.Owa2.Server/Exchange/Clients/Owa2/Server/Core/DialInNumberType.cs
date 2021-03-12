using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003BA RID: 954
	[KnownType(typeof(JsonFaultResponse))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DialInNumberType
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00076A1D File Offset: 0x00074C1D
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x00076A25 File Offset: 0x00074C25
		[DataMember]
		public string RegionName { get; set; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x00076A2E File Offset: 0x00074C2E
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x00076A36 File Offset: 0x00074C36
		[DataMember]
		public string Number { get; set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x00076A3F File Offset: 0x00074C3F
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x00076A47 File Offset: 0x00074C47
		[DataMember]
		public string Language { get; set; }
	}
}
