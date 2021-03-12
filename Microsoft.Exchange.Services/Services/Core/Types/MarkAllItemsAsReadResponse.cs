using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000530 RID: 1328
	[XmlType("MarkAllItemsAsReadResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class MarkAllItemsAsReadResponse : BaseResponseMessage
	{
		// Token: 0x060025EA RID: 9706 RVA: 0x000A610A File Offset: 0x000A430A
		public MarkAllItemsAsReadResponse() : base(ResponseType.MarkAllItemsAsReadResponseMessage)
		{
		}
	}
}
