using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B8D RID: 2957
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExportItemsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EE4 RID: 12004
		[DataMember(IsRequired = true, Order = 0)]
		public ExportItemsRequest Body;
	}
}
