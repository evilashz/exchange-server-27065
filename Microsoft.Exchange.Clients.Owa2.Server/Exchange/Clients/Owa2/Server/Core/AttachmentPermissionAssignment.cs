using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003AF RID: 943
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentPermissionAssignment
	{
		// Token: 0x040010ED RID: 4333
		[DataMember]
		public AttachmentPermissionLevel PermissionLevel;

		// Token: 0x040010EE RID: 4334
		[DataMember]
		public string ResourceLocation;

		// Token: 0x040010EF RID: 4335
		[DataMember]
		public string ResourceEndpointUrl;
	}
}
