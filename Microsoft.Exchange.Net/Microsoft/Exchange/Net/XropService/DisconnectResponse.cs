using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000BA1 RID: 2977
	[DataContract(Name = "DisconnectResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/xrop")]
	[CLSCompliant(false)]
	public sealed class DisconnectResponse
	{
		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x000A85D2 File Offset: 0x000A67D2
		// (set) Token: 0x06003FE2 RID: 16354 RVA: 0x000A85DA File Offset: 0x000A67DA
		[DataMember]
		public uint ServiceCode { get; set; }

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x000A85E3 File Offset: 0x000A67E3
		// (set) Token: 0x06003FE4 RID: 16356 RVA: 0x000A85EB File Offset: 0x000A67EB
		[DataMember]
		public uint ErrorCode { get; set; }

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x000A85F4 File Offset: 0x000A67F4
		// (set) Token: 0x06003FE6 RID: 16358 RVA: 0x000A85FC File Offset: 0x000A67FC
		[DataMember]
		public uint Context { get; set; }
	}
}
