using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200026D RID: 621
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateReferenceAttachmentFromLocalFileRequestWrapper
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x00053913 File Offset: 0x00051B13
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x0005391B File Offset: 0x00051B1B
		[DataMember(Name = "requestObject")]
		public CreateReferenceAttachmentFromLocalFileRequest RequestObject { get; set; }
	}
}
