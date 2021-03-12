using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B8E RID: 2958
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExportItemsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EE5 RID: 12005
		[DataMember(IsRequired = true, Order = 0)]
		public ExportItemsResponse Body;
	}
}
