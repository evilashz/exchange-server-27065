using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000574 RID: 1396
	[XmlType(TypeName = "UpdateGroupMailboxResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UpdateGroupMailboxResponse : BaseResponseMessage
	{
		// Token: 0x060026F0 RID: 9968 RVA: 0x000A6C02 File Offset: 0x000A4E02
		public UpdateGroupMailboxResponse() : base(ResponseType.UpdateGroupMailboxResponseMessage)
		{
		}
	}
}
