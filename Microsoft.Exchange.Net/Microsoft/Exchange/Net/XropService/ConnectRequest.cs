using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B97 RID: 2967
	[CLSCompliant(false)]
	[DataContract(Name = "ConnectRequest", Namespace = "http://schemas.microsoft.com/exchange/2010/xrop")]
	public sealed class ConnectRequest
	{
		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x000A8274 File Offset: 0x000A6474
		// (set) Token: 0x06003F7C RID: 16252 RVA: 0x000A827C File Offset: 0x000A647C
		[DataMember]
		public bool Interactive { get; set; }

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000A8285 File Offset: 0x000A6485
		// (set) Token: 0x06003F7E RID: 16254 RVA: 0x000A828D File Offset: 0x000A648D
		[DataMember]
		public string UserDN { get; set; }

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000A8296 File Offset: 0x000A6496
		// (set) Token: 0x06003F80 RID: 16256 RVA: 0x000A829E File Offset: 0x000A649E
		[DataMember]
		public uint Flags { get; set; }

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x000A82A7 File Offset: 0x000A64A7
		// (set) Token: 0x06003F82 RID: 16258 RVA: 0x000A82AF File Offset: 0x000A64AF
		[DataMember]
		public uint ConMod { get; set; }

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x000A82B8 File Offset: 0x000A64B8
		// (set) Token: 0x06003F84 RID: 16260 RVA: 0x000A82C0 File Offset: 0x000A64C0
		[DataMember]
		public uint Limit { get; set; }

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06003F85 RID: 16261 RVA: 0x000A82C9 File Offset: 0x000A64C9
		// (set) Token: 0x06003F86 RID: 16262 RVA: 0x000A82D1 File Offset: 0x000A64D1
		[DataMember]
		public uint Cpid { get; set; }

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000A82DA File Offset: 0x000A64DA
		// (set) Token: 0x06003F88 RID: 16264 RVA: 0x000A82E2 File Offset: 0x000A64E2
		[DataMember]
		public uint LcidString { get; set; }

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x000A82EB File Offset: 0x000A64EB
		// (set) Token: 0x06003F8A RID: 16266 RVA: 0x000A82F3 File Offset: 0x000A64F3
		[DataMember]
		public uint LcidSort { get; set; }

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000A82FC File Offset: 0x000A64FC
		// (set) Token: 0x06003F8C RID: 16268 RVA: 0x000A8304 File Offset: 0x000A6504
		[DataMember]
		public uint IcxrLink { get; set; }

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000A830D File Offset: 0x000A650D
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x000A8315 File Offset: 0x000A6515
		[DataMember]
		public ushort FCanConvertCodePages { get; set; }

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000A831E File Offset: 0x000A651E
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x000A8326 File Offset: 0x000A6526
		[DataMember]
		public byte[] ClientVersion { get; set; }

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000A832F File Offset: 0x000A652F
		// (set) Token: 0x06003F92 RID: 16274 RVA: 0x000A8337 File Offset: 0x000A6537
		[DataMember]
		public uint TimeStamp { get; set; }

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000A8340 File Offset: 0x000A6540
		// (set) Token: 0x06003F94 RID: 16276 RVA: 0x000A8348 File Offset: 0x000A6548
		[DataMember]
		public byte[] AuxIn { get; set; }

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x000A8351 File Offset: 0x000A6551
		// (set) Token: 0x06003F96 RID: 16278 RVA: 0x000A8359 File Offset: 0x000A6559
		[DataMember]
		public uint AuxOutMaxSize { get; set; }
	}
}
