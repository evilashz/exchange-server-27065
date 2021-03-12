using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B4 RID: 692
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarPublishingRequestWrapper
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0005419A File Offset: 0x0005239A
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x000541A2 File Offset: 0x000523A2
		[DataMember(Name = "request")]
		public SetCalendarPublishingRequest Request { get; set; }
	}
}
