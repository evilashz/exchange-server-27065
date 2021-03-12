using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A4F RID: 2639
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeletePlaceRequest
	{
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06004ACB RID: 19147 RVA: 0x00104CF4 File Offset: 0x00102EF4
		// (set) Token: 0x06004ACC RID: 19148 RVA: 0x00104CFC File Offset: 0x00102EFC
		[DataMember]
		public string Id { get; set; }
	}
}
