using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200041B RID: 1051
	[XmlType("ExpandDLType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExpandDLRequest : BaseRequest
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0009FCC1 File Offset: 0x0009DEC1
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x0009FCC9 File Offset: 0x0009DEC9
		[DataMember(Name = "Mailbox", IsRequired = true)]
		[XmlElement("Mailbox")]
		public EmailAddressWrapper Mailbox { get; set; }

		// Token: 0x06001E34 RID: 7732 RVA: 0x0009FCD2 File Offset: 0x0009DED2
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new ExpandDL(callContext, this);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0009FCDC File Offset: 0x0009DEDC
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			ItemId itemId = this.Mailbox.ItemId;
			if (itemId != null)
			{
				return BaseRequest.GetServerInfoForItemId(callContext, itemId);
			}
			return null;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0009FD01 File Offset: 0x0009DF01
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
