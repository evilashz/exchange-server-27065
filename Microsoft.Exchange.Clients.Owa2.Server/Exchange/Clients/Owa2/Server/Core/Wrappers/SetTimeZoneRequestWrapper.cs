using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002C1 RID: 705
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetTimeZoneRequestWrapper
	{
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x000542F0 File Offset: 0x000524F0
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x000542F8 File Offset: 0x000524F8
		[DataMember(Name = "timezone")]
		public string Timezone { get; set; }
	}
}
