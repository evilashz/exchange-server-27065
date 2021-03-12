using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000436 RID: 1078
	[XmlType(TypeName = "GetHoldOnMailboxesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "GetHoldOnMailboxesRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetHoldOnMailboxesRequest : BaseRequest
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x000A100A File Offset: 0x0009F20A
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x000A1012 File Offset: 0x0009F212
		[DataMember(Name = "HoldId", IsRequired = true)]
		[XmlElement("HoldId")]
		public string HoldId
		{
			get
			{
				return this.holdId;
			}
			set
			{
				this.holdId = value;
			}
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000A101B File Offset: 0x0009F21B
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetHoldOnMailboxes(callContext, this);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x000A1024 File Offset: 0x0009F224
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000A1027 File Offset: 0x0009F227
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return null;
		}

		// Token: 0x040013F2 RID: 5106
		private string holdId;
	}
}
