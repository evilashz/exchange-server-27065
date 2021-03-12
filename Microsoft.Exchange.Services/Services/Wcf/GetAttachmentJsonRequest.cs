using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB5 RID: 2997
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAttachmentJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F0C RID: 12044
		[DataMember(IsRequired = true, Order = 0)]
		public GetAttachmentRequest Body;
	}
}
