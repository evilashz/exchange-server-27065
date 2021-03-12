using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000469 RID: 1129
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("PlayOnPhoneRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class PlayOnPhoneRequest : BaseRequest
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x000A2406 File Offset: 0x000A0606
		// (set) Token: 0x0600213F RID: 8511 RVA: 0x000A240E File Offset: 0x000A060E
		[XmlElement("ItemId")]
		[DataMember(Name = "ItemId", IsRequired = true, Order = 1)]
		public ItemId ItemId { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x000A2417 File Offset: 0x000A0617
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x000A241F File Offset: 0x000A061F
		[XmlElement("DialString")]
		[DataMember(Name = "DialString", IsRequired = true, Order = 2)]
		public string DialString { get; set; }

		// Token: 0x06002142 RID: 8514 RVA: 0x000A2428 File Offset: 0x000A0628
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new PlayOnPhone(callContext, this);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000A2431 File Offset: 0x000A0631
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000A2434 File Offset: 0x000A0634
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
