using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200048B RID: 1163
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SyncAutoCompleteRecipientType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SyncAutoCompleteRecipientsRequest : SyncPersonaContactsRequestBase
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x000A32AE File Offset: 0x000A14AE
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x000A32B6 File Offset: 0x000A14B6
		[XmlElement("FullSyncOnly", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "FullSyncOnly", IsRequired = false)]
		public bool FullSyncOnly { get; set; }

		// Token: 0x0600228F RID: 8847 RVA: 0x000A32BF File Offset: 0x000A14BF
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SyncAutoCompleteRecipients(callContext, this);
		}
	}
}
