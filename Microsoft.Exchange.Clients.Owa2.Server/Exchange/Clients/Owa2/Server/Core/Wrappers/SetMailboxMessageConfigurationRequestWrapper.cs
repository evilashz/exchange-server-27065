using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002BA RID: 698
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxMessageConfigurationRequestWrapper
	{
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00054230 File Offset: 0x00052430
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x00054238 File Offset: 0x00052438
		[DataMember(Name = "request")]
		public SetMailboxMessageConfigurationRequest Request { get; set; }
	}
}
