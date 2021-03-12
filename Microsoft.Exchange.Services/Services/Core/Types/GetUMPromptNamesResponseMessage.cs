using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200051E RID: 1310
	[XmlType("GetUMPromptNamesResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMPromptNamesResponseMessage : ResponseMessage
	{
		// Token: 0x06002596 RID: 9622 RVA: 0x000A5C4F File Offset: 0x000A3E4F
		public GetUMPromptNamesResponseMessage()
		{
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000A5C57 File Offset: 0x000A3E57
		internal GetUMPromptNamesResponseMessage(ServiceResultCode code, ServiceError error, GetUMPromptNamesResponseMessage response) : base(code, error)
		{
			if (response != null)
			{
				this.PromptNames = response.PromptNames;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x000A5C70 File Offset: 0x000A3E70
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x000A5C78 File Offset: 0x000A3E78
		[XmlArray(ElementName = "PromptNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlArrayItem(ElementName = "String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] PromptNames { get; set; }

		// Token: 0x0600259A RID: 9626 RVA: 0x000A5C81 File Offset: 0x000A3E81
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetUMPromptNamesResponseMessage;
		}
	}
}
