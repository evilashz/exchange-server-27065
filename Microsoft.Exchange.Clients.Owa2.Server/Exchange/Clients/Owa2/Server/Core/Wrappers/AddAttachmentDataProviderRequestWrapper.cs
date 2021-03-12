using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200025B RID: 603
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddAttachmentDataProviderRequestWrapper
	{
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00053696 File Offset: 0x00051896
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x0005369E File Offset: 0x0005189E
		[DataMember(Name = "attachmentDataProvider")]
		public AttachmentDataProvider AttachmentDataProvider { get; set; }
	}
}
