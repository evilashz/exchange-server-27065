using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB6 RID: 2998
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAttachmentJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F0D RID: 12045
		[DataMember(IsRequired = true, Order = 0)]
		public GetAttachmentResponse Body;
	}
}
