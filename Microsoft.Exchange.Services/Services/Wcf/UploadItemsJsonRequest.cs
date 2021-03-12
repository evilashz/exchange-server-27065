using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B8B RID: 2955
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UploadItemsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EE2 RID: 12002
		[DataMember(IsRequired = true, Order = 0)]
		public UploadItemsRequest Body;
	}
}
