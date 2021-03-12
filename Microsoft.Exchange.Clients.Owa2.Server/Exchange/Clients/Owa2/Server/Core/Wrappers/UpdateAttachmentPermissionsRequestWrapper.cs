using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002CC RID: 716
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateAttachmentPermissionsRequestWrapper
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x00054458 File Offset: 0x00052658
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x00054460 File Offset: 0x00052660
		[DataMember(Name = "permissionsRequest")]
		public UpdateAttachmentPermissionsRequest PermissionsRequest { get; set; }
	}
}
