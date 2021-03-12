using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BEB RID: 3051
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RefreshSharingFolderJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F42 RID: 12098
		[DataMember(IsRequired = true, Order = 0)]
		public RefreshSharingFolderRequest Body;
	}
}
