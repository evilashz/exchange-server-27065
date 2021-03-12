using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000564 RID: 1380
	[XmlType("SyncAutoCompleteRecipientsResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncAutoCompleteRecipientsResponseMessage : SyncPersonaContactsResponseBase
	{
		// Token: 0x06002699 RID: 9881 RVA: 0x000A67D8 File Offset: 0x000A49D8
		public SyncAutoCompleteRecipientsResponseMessage()
		{
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000A67E0 File Offset: 0x000A49E0
		internal SyncAutoCompleteRecipientsResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000A67EA File Offset: 0x000A49EA
		public override ResponseType GetResponseType()
		{
			return ResponseType.SyncAutoCompleteRecipientsResponseMessage;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000A67F1 File Offset: 0x000A49F1
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x000A67F9 File Offset: 0x000A49F9
		[XmlArray(ElementName = "AutoCompleteRecipient")]
		[XmlArrayItem(ElementName = "AutoCompleteRecipient", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(EmitDefaultValue = false)]
		public AutoCompleteRecipient[] Recipients { get; set; }
	}
}
