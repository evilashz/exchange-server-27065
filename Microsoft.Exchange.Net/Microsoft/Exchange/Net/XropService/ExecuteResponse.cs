using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B9D RID: 2973
	[DataContract(Name = "ExecuteResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/xrop")]
	[CLSCompliant(false)]
	public sealed class ExecuteResponse
	{
		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x000A8508 File Offset: 0x000A6708
		// (set) Token: 0x06003FCA RID: 16330 RVA: 0x000A8510 File Offset: 0x000A6710
		[DataMember]
		public uint ServiceCode { get; set; }

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x000A8519 File Offset: 0x000A6719
		// (set) Token: 0x06003FCC RID: 16332 RVA: 0x000A8521 File Offset: 0x000A6721
		[DataMember]
		public uint ErrorCode { get; set; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06003FCD RID: 16333 RVA: 0x000A852A File Offset: 0x000A672A
		// (set) Token: 0x06003FCE RID: 16334 RVA: 0x000A8532 File Offset: 0x000A6732
		[DataMember]
		public uint Context { get; set; }

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06003FCF RID: 16335 RVA: 0x000A853B File Offset: 0x000A673B
		// (set) Token: 0x06003FD0 RID: 16336 RVA: 0x000A8543 File Offset: 0x000A6743
		[DataMember]
		public uint Flags { get; set; }

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x000A854C File Offset: 0x000A674C
		// (set) Token: 0x06003FD2 RID: 16338 RVA: 0x000A8554 File Offset: 0x000A6754
		[DataMember]
		public byte[] Out { get; set; }

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x000A855D File Offset: 0x000A675D
		// (set) Token: 0x06003FD4 RID: 16340 RVA: 0x000A8565 File Offset: 0x000A6765
		[DataMember]
		public byte[] AuxOut { get; set; }

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x000A856E File Offset: 0x000A676E
		// (set) Token: 0x06003FD6 RID: 16342 RVA: 0x000A8576 File Offset: 0x000A6776
		[DataMember]
		public uint TransTime { get; set; }
	}
}
