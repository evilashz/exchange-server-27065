using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB1 RID: 2993
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateAttachmentJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F08 RID: 12040
		[DataMember(IsRequired = true, Order = 0)]
		public CreateAttachmentRequest Body;
	}
}
