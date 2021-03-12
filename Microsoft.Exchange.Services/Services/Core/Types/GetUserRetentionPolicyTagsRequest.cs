using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200045B RID: 1115
	[XmlType(TypeName = "GetUserRetentionPolicyTagsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "GetUserRetentionPolicyTagsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetUserRetentionPolicyTagsRequest : BaseRequest
	{
		// Token: 0x060020DF RID: 8415 RVA: 0x000A1FE4 File Offset: 0x000A01E4
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUserRetentionPolicyTags(callContext, this);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000A1FED File Offset: 0x000A01ED
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000A1FF0 File Offset: 0x000A01F0
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
