using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000443 RID: 1091
	[XmlType("GetPeopleConnectStateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public sealed class GetPeopleConnectStateRequest : BaseRequest
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x000A17F5 File Offset: 0x0009F9F5
		// (set) Token: 0x06002005 RID: 8197 RVA: 0x000A17FD File Offset: 0x0009F9FD
		[XmlElement]
		[DataMember(Name = "Provider", IsRequired = true)]
		public string Provider { get; set; }

		// Token: 0x06002006 RID: 8198 RVA: 0x000A1806 File Offset: 0x0009FA06
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetPeopleConnectState(callContext, this);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000A180F File Offset: 0x0009FA0F
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000A1812 File Offset: 0x0009FA12
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
