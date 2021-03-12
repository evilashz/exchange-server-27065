using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000292 RID: 658
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUcwaUserConfigurationRequestWrapper
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00053D5A File Offset: 0x00051F5A
		// (set) Token: 0x0600179C RID: 6044 RVA: 0x00053D62 File Offset: 0x00051F62
		[DataMember(Name = "sipUri")]
		public string SipUri { get; set; }
	}
}
