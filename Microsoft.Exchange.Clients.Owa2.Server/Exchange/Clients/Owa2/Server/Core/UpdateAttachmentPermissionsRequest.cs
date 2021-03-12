using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000411 RID: 1041
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateAttachmentPermissionsRequest
	{
		// Token: 0x0400132D RID: 4909
		[DataMember]
		public AttachmentDataProviderPermissions[] AttachmentDataProviderPermissions;

		// Token: 0x0400132E RID: 4910
		[DataMember]
		public string[] UserIds;
	}
}
