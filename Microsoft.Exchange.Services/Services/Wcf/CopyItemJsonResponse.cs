using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BB0 RID: 2992
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CopyItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F07 RID: 12039
		[DataMember(IsRequired = true, Order = 0)]
		public CopyItemResponse Body;
	}
}
