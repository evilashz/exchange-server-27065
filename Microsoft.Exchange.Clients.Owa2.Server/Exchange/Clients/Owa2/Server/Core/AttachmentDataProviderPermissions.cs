using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003A7 RID: 935
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentDataProviderPermissions
	{
		// Token: 0x040010D9 RID: 4313
		[DataMember]
		public AttachmentPermissionAssignment[] PermissionAssignments;

		// Token: 0x040010DA RID: 4314
		[DataMember]
		public string AttachmentDataProviderId;
	}
}
