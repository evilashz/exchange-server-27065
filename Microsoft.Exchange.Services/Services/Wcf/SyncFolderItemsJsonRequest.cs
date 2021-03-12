using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC9 RID: 3017
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncFolderItemsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F20 RID: 12064
		[DataMember(IsRequired = true, Order = 0)]
		public SyncFolderItemsRequest Body;
	}
}
