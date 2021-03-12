using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003EE RID: 1006
	[XmlInclude(typeof(RemoveAggregatedAccountRequest))]
	[XmlType("BaseAggregatedAccountRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(CreateUnifiedMailboxRequest))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlInclude(typeof(SetAggregatedAccountRequest))]
	[XmlInclude(typeof(GetAggregatedAccountRequest))]
	[XmlInclude(typeof(AddAggregatedAccountRequest))]
	public class BaseAggregatedAccountRequest : BaseRequest
	{
		// Token: 0x06001C2D RID: 7213 RVA: 0x0009E144 File Offset: 0x0009C344
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0009E147 File Offset: 0x0009C347
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
