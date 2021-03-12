using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200046A RID: 1130
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ProvisionRequest : BaseRequest
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x000A243F File Offset: 0x000A063F
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x000A2447 File Offset: 0x000A0647
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 1)]
		public string DeviceFriendlyName { get; set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x000A2450 File Offset: 0x000A0650
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x000A2458 File Offset: 0x000A0658
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 2)]
		public string DeviceID { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x000A2461 File Offset: 0x000A0661
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x000A2469 File Offset: 0x000A0669
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 3)]
		public string DeviceImei { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x000A2472 File Offset: 0x000A0672
		// (set) Token: 0x0600214D RID: 8525 RVA: 0x000A247A File Offset: 0x000A067A
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 4)]
		public string DeviceMobileOperator { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x000A2483 File Offset: 0x000A0683
		// (set) Token: 0x0600214F RID: 8527 RVA: 0x000A248B File Offset: 0x000A068B
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 5)]
		public string DeviceOS { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000A2494 File Offset: 0x000A0694
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x000A249C File Offset: 0x000A069C
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 6)]
		public string DeviceOSLanguage { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x000A24A5 File Offset: 0x000A06A5
		// (set) Token: 0x06002153 RID: 8531 RVA: 0x000A24AD File Offset: 0x000A06AD
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 7)]
		public string DevicePhoneNumber { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000A24B6 File Offset: 0x000A06B6
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x000A24BE File Offset: 0x000A06BE
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 8)]
		public string DeviceType { get; set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x000A24C7 File Offset: 0x000A06C7
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x000A24CF File Offset: 0x000A06CF
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 9)]
		public string DeviceModel { get; set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000A24D8 File Offset: 0x000A06D8
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x000A24E0 File Offset: 0x000A06E0
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 10)]
		public string ClientVersion { get; set; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x000A24E9 File Offset: 0x000A06E9
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x000A24F1 File Offset: 0x000A06F1
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 11)]
		public bool HasPAL { get; set; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x000A24FA File Offset: 0x000A06FA
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x000A2502 File Offset: 0x000A0702
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 13)]
		public bool SpecifyProtocol { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x000A250B File Offset: 0x000A070B
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x000A2513 File Offset: 0x000A0713
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 12)]
		public string Protocol { get; set; }

		// Token: 0x06002160 RID: 8544 RVA: 0x000A251C File Offset: 0x000A071C
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new Provision(callContext, this);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000A2525 File Offset: 0x000A0725
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000A2528 File Offset: 0x000A0728
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
