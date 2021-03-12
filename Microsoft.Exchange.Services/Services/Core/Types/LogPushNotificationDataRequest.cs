using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000463 RID: 1123
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class LogPushNotificationDataRequest : BaseRequest
	{
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x000A21F0 File Offset: 0x000A03F0
		// (set) Token: 0x0600210E RID: 8462 RVA: 0x000A21F8 File Offset: 0x000A03F8
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public string AppId { get; set; }

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x000A2201 File Offset: 0x000A0401
		// (set) Token: 0x06002110 RID: 8464 RVA: 0x000A2209 File Offset: 0x000A0409
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 2)]
		public string DataType { get; set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000A2212 File Offset: 0x000A0412
		// (set) Token: 0x06002112 RID: 8466 RVA: 0x000A221A File Offset: 0x000A041A
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 3)]
		public string[] KeyValuePairs { get; set; }

		// Token: 0x06002113 RID: 8467 RVA: 0x000A2223 File Offset: 0x000A0423
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new LogPushNotificationData(callContext, this);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000A222C File Offset: 0x000A042C
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000A222F File Offset: 0x000A042F
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
