using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BEC RID: 3052
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RefreshSharingFolderJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F43 RID: 12099
		[DataMember(IsRequired = true, Order = 0)]
		public RefreshSharingFolderResponseMessage Body;
	}
}
