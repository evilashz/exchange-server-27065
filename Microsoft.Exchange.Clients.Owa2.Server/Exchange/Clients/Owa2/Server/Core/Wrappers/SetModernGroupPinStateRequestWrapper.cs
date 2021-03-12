using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002BC RID: 700
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetModernGroupPinStateRequestWrapper
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x00054262 File Offset: 0x00052462
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x0005426A File Offset: 0x0005246A
		[DataMember(Name = "smtpAddress")]
		public string SmtpAddress { get; set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00054273 File Offset: 0x00052473
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x0005427B File Offset: 0x0005247B
		[DataMember(Name = "isPinned")]
		public bool IsPinned { get; set; }
	}
}
