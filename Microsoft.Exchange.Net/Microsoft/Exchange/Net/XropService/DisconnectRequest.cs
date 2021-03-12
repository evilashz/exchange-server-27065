using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B9F RID: 2975
	[DataContract(Name = "DisconnectRequest", Namespace = "http://schemas.microsoft.com/exchange/2010/xrop")]
	[CLSCompliant(false)]
	public sealed class DisconnectRequest
	{
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x000A85A0 File Offset: 0x000A67A0
		// (set) Token: 0x06003FDC RID: 16348 RVA: 0x000A85A8 File Offset: 0x000A67A8
		[DataMember]
		public uint Context { get; set; }
	}
}
