using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000264 RID: 612
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ConnectedAccountsNotificationRequestWrapper
	{
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00053799 File Offset: 0x00051999
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x000537A1 File Offset: 0x000519A1
		[DataMember(Name = "isOWALogon")]
		public bool IsOWALogon { get; set; }
	}
}
