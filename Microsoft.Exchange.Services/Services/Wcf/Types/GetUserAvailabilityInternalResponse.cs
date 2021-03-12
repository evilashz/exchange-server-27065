using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A2E RID: 2606
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserAvailabilityInternalResponse : ResponseMessage
	{
		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x0010298E File Offset: 0x00100B8E
		// (set) Token: 0x06004987 RID: 18823 RVA: 0x00102996 File Offset: 0x00100B96
		[DataMember]
		public UserAvailabilityInternalResponse[] Responses { get; set; }
	}
}
