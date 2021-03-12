using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B22 RID: 2850
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupUnseenItemsResponse : ResponseMessage
	{
		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x060050D1 RID: 20689 RVA: 0x00109F68 File Offset: 0x00108168
		// (set) Token: 0x060050D2 RID: 20690 RVA: 0x00109F70 File Offset: 0x00108170
		[DataMember(IsRequired = true)]
		public UnseenDataType UnseenData { get; set; }
	}
}
