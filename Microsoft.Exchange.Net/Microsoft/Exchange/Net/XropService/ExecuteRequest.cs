using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B9B RID: 2971
	[DataContract(Name = "ExecuteRequest", Namespace = "http://schemas.microsoft.com/exchange/2010/xrop")]
	[CLSCompliant(false)]
	public sealed class ExecuteRequest
	{
		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x000A8481 File Offset: 0x000A6681
		// (set) Token: 0x06003FBA RID: 16314 RVA: 0x000A8489 File Offset: 0x000A6689
		[DataMember]
		public uint Context { get; set; }

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06003FBB RID: 16315 RVA: 0x000A8492 File Offset: 0x000A6692
		// (set) Token: 0x06003FBC RID: 16316 RVA: 0x000A849A File Offset: 0x000A669A
		[DataMember]
		public uint Flags { get; set; }

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x000A84A3 File Offset: 0x000A66A3
		// (set) Token: 0x06003FBE RID: 16318 RVA: 0x000A84AB File Offset: 0x000A66AB
		[DataMember]
		public byte[] In { get; set; }

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06003FBF RID: 16319 RVA: 0x000A84B4 File Offset: 0x000A66B4
		// (set) Token: 0x06003FC0 RID: 16320 RVA: 0x000A84BC File Offset: 0x000A66BC
		[DataMember]
		public uint MaxSizeOut { get; set; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x000A84C5 File Offset: 0x000A66C5
		// (set) Token: 0x06003FC2 RID: 16322 RVA: 0x000A84CD File Offset: 0x000A66CD
		[DataMember]
		public byte[] AuxIn { get; set; }

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x000A84D6 File Offset: 0x000A66D6
		// (set) Token: 0x06003FC4 RID: 16324 RVA: 0x000A84DE File Offset: 0x000A66DE
		[DataMember]
		public uint MaxSizeAuxOut { get; set; }
	}
}
