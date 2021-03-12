using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000291 RID: 657
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetTimeZoneRequestWrapper
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00053D41 File Offset: 0x00051F41
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x00053D49 File Offset: 0x00051F49
		[DataMember(Name = "needTimeZoneList")]
		public bool NeedTimeZoneList { get; set; }
	}
}
