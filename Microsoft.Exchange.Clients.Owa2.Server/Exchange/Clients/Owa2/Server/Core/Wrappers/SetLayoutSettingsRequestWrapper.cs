using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x020002B7 RID: 695
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetLayoutSettingsRequestWrapper
	{
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x000541E5 File Offset: 0x000523E5
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x000541ED File Offset: 0x000523ED
		[DataMember(Name = "layoutSettings")]
		public LayoutSettingsType LayoutSettings { get; set; }
	}
}
