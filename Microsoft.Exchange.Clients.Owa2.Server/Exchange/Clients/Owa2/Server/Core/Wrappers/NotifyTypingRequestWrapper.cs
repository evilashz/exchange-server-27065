using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200029F RID: 671
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NotifyTypingRequestWrapper
	{
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x00053F05 File Offset: 0x00052105
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x00053F0D File Offset: 0x0005210D
		[DataMember(Name = "chatSessionId")]
		public int ChatSessionId { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00053F16 File Offset: 0x00052116
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x00053F1E File Offset: 0x0005211E
		[DataMember(Name = "typingCancelled")]
		public bool TypingCancelled { get; set; }
	}
}
