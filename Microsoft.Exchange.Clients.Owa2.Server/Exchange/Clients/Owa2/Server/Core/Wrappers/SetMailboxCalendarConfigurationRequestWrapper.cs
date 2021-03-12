using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B9 RID: 697
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxCalendarConfigurationRequestWrapper
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x00054217 File Offset: 0x00052417
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x0005421F File Offset: 0x0005241F
		[DataMember(Name = "request")]
		public SetMailboxCalendarConfigurationRequest Request { get; set; }
	}
}
