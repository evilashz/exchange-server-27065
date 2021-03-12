using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009EE RID: 2542
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetPeopleIKnowGraphResponse
	{
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x060047CB RID: 18379 RVA: 0x00100803 File Offset: 0x000FEA03
		// (set) Token: 0x060047CC RID: 18380 RVA: 0x0010080B File Offset: 0x000FEA0B
		[DataMember]
		public string SerializedPeopleIKnowGraph { get; set; }
	}
}
