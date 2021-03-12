using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB2 RID: 2994
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateAttachmentJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F09 RID: 12041
		[DataMember(IsRequired = true, Order = 0)]
		public CreateAttachmentResponse Body;
	}
}
