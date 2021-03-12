using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200049F RID: 1183
	[XmlType("ValidateUMPinType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ValidateUMPinRequest : BaseRequest
	{
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x000A3F3C File Offset: 0x000A213C
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x000A3F44 File Offset: 0x000A2144
		[XmlElement("PinInfo")]
		[DataMember(Name = "PinInfo")]
		public PINInfo PinInfo { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000A3F4D File Offset: 0x000A214D
		// (set) Token: 0x06002378 RID: 9080 RVA: 0x000A3F55 File Offset: 0x000A2155
		[DataMember(Name = "UserUMMailboxPolicyGuid")]
		[XmlElement("UserUMMailboxPolicyGuid")]
		public Guid UserUMMailboxPolicyGuid { get; set; }

		// Token: 0x06002379 RID: 9081 RVA: 0x000A3F5E File Offset: 0x000A215E
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new ValidateUMPin(callContext, this);
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000A3F67 File Offset: 0x000A2167
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x000A3F6A File Offset: 0x000A216A
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
