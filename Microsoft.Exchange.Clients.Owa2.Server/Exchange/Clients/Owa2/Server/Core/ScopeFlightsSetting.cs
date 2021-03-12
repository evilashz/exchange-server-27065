using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003FB RID: 1019
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ScopeFlightsSetting
	{
		// Token: 0x06002107 RID: 8455 RVA: 0x00079466 File Offset: 0x00077666
		public ScopeFlightsSetting(string scope, string[] flights)
		{
			this.Scope = scope;
			this.Flights = flights;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x0007947C File Offset: 0x0007767C
		// (set) Token: 0x06002109 RID: 8457 RVA: 0x00079484 File Offset: 0x00077684
		[DataMember]
		public string Scope { get; set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x0007948D File Offset: 0x0007768D
		// (set) Token: 0x0600210B RID: 8459 RVA: 0x00079495 File Offset: 0x00077695
		[DataMember]
		public string[] Flights { get; set; }
	}
}
