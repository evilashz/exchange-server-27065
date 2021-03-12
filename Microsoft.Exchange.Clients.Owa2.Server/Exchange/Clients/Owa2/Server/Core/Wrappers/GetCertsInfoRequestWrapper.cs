using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200027F RID: 639
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCertsInfoRequestWrapper
	{
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00053B08 File Offset: 0x00051D08
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x00053B10 File Offset: 0x00051D10
		[DataMember(Name = "certRawData")]
		public string CertRawData { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00053B19 File Offset: 0x00051D19
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x00053B21 File Offset: 0x00051D21
		[DataMember(Name = "isSend")]
		public bool IsSend { get; set; }
	}
}
