using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BCA RID: 3018
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncFolderItemsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F21 RID: 12065
		[DataMember(IsRequired = true, Order = 0)]
		public SyncFolderItemsResponse Body;
	}
}
