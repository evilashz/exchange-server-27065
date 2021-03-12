using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000444 RID: 1092
	[XmlType("GetPeopleConnectTokenType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public sealed class GetPeopleConnectTokenRequest : BaseRequest
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x000A1824 File Offset: 0x0009FA24
		// (set) Token: 0x0600200B RID: 8203 RVA: 0x000A182C File Offset: 0x0009FA2C
		[XmlElement]
		[DataMember(Name = "Provider", IsRequired = true)]
		public string Provider { get; set; }

		// Token: 0x0600200C RID: 8204 RVA: 0x000A1835 File Offset: 0x0009FA35
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetPeopleConnectToken(callContext, this);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000A183E File Offset: 0x0009FA3E
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000A1841 File Offset: 0x0009FA41
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
