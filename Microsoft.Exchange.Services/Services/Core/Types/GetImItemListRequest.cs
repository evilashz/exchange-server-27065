using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000437 RID: 1079
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetImItemListRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetImItemListRequest : BaseRequest
	{
		// Token: 0x06001FAD RID: 8109 RVA: 0x000A1032 File Offset: 0x0009F232
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetImItemListCommand(callContext, this);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000A103B File Offset: 0x0009F23B
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000A1043 File Offset: 0x0009F243
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x040013F3 RID: 5107
		[DataMember(Name = "ExtendedProperties", IsRequired = false, Order = 1)]
		[XmlArray]
		[XmlArrayItem("ExtendedProperty", typeof(ExtendedPropertyUri), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ExtendedPropertyUri[] ExtendedProperties;
	}
}
