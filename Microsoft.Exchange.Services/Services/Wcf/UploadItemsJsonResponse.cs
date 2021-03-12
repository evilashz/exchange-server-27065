using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B8C RID: 2956
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UploadItemsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EE3 RID: 12003
		[DataMember(IsRequired = true, Order = 0)]
		public UploadItemsResponse Body;
	}
}
