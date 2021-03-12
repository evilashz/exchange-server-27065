using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B8 RID: 696
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxAutoReplyConfigurationRequestWrapper
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x000541FE File Offset: 0x000523FE
		// (set) Token: 0x0600182A RID: 6186 RVA: 0x00054206 File Offset: 0x00052406
		[DataMember(Name = "request")]
		public SetMailboxAutoReplyConfigurationRequest Request { get; set; }
	}
}
