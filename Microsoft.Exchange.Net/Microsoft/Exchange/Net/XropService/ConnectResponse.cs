using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B99 RID: 2969
	[CLSCompliant(false)]
	[DataContract(Name = "ConnectResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/xrop")]
	public sealed class ConnectResponse
	{
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x000A8383 File Offset: 0x000A6583
		// (set) Token: 0x06003F9C RID: 16284 RVA: 0x000A838B File Offset: 0x000A658B
		[DataMember]
		public uint ServiceCode { get; set; }

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x000A8394 File Offset: 0x000A6594
		// (set) Token: 0x06003F9E RID: 16286 RVA: 0x000A839C File Offset: 0x000A659C
		[DataMember]
		public uint ErrorCode { get; set; }

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x000A83A5 File Offset: 0x000A65A5
		// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x000A83AD File Offset: 0x000A65AD
		[DataMember]
		public uint Context { get; set; }

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x000A83B6 File Offset: 0x000A65B6
		// (set) Token: 0x06003FA2 RID: 16290 RVA: 0x000A83BE File Offset: 0x000A65BE
		[DataMember]
		public uint PollsMax { get; set; }

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06003FA3 RID: 16291 RVA: 0x000A83C7 File Offset: 0x000A65C7
		// (set) Token: 0x06003FA4 RID: 16292 RVA: 0x000A83CF File Offset: 0x000A65CF
		[DataMember]
		public uint Retry { get; set; }

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x000A83D8 File Offset: 0x000A65D8
		// (set) Token: 0x06003FA6 RID: 16294 RVA: 0x000A83E0 File Offset: 0x000A65E0
		[DataMember]
		public uint RetryDelay { get; set; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06003FA7 RID: 16295 RVA: 0x000A83E9 File Offset: 0x000A65E9
		// (set) Token: 0x06003FA8 RID: 16296 RVA: 0x000A83F1 File Offset: 0x000A65F1
		[DataMember]
		public ushort Icxr { get; set; }

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06003FA9 RID: 16297 RVA: 0x000A83FA File Offset: 0x000A65FA
		// (set) Token: 0x06003FAA RID: 16298 RVA: 0x000A8402 File Offset: 0x000A6602
		[DataMember]
		public string DNPrefix { get; set; }

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06003FAB RID: 16299 RVA: 0x000A840B File Offset: 0x000A660B
		// (set) Token: 0x06003FAC RID: 16300 RVA: 0x000A8413 File Offset: 0x000A6613
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06003FAD RID: 16301 RVA: 0x000A841C File Offset: 0x000A661C
		// (set) Token: 0x06003FAE RID: 16302 RVA: 0x000A8424 File Offset: 0x000A6624
		[DataMember]
		public byte[] ServerVersion { get; set; }

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06003FAF RID: 16303 RVA: 0x000A842D File Offset: 0x000A662D
		// (set) Token: 0x06003FB0 RID: 16304 RVA: 0x000A8435 File Offset: 0x000A6635
		[DataMember]
		public byte[] BestVersion { get; set; }

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x000A843E File Offset: 0x000A663E
		// (set) Token: 0x06003FB2 RID: 16306 RVA: 0x000A8446 File Offset: 0x000A6646
		[DataMember]
		public uint TimeStamp { get; set; }

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x000A844F File Offset: 0x000A664F
		// (set) Token: 0x06003FB4 RID: 16308 RVA: 0x000A8457 File Offset: 0x000A6657
		[DataMember]
		public byte[] AuxOut { get; set; }
	}
}
