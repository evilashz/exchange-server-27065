using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB4 RID: 2996
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteAttachmentJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F0B RID: 12043
		[DataMember(IsRequired = true, Order = 0)]
		public DeleteAttachmentResponse Body;
	}
}
