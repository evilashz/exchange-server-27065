using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F4 RID: 1012
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("AddImGroupRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddImGroupRequest : BaseRequest
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0009E368 File Offset: 0x0009C568
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x0009E370 File Offset: 0x0009C570
		[XmlElement(ElementName = "DisplayName")]
		[DataMember(Name = "DisplayName", IsRequired = true, Order = 1)]
		public string DisplayName { get; set; }

		// Token: 0x06001C67 RID: 7271 RVA: 0x0009E379 File Offset: 0x0009C579
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new AddImGroupCommand(callContext, this);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0009E382 File Offset: 0x0009C582
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0009E38A File Offset: 0x0009C58A
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}
	}
}
