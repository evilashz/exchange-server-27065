using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200057C RID: 1404
	[XmlType(TypeName = "UpdateMailboxAssociationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UpdateMailboxAssociationResponse : BaseResponseMessage
	{
		// Token: 0x06002714 RID: 10004 RVA: 0x000A6E38 File Offset: 0x000A5038
		public UpdateMailboxAssociationResponse() : base(ResponseType.UpdateMailboxAssociationResponseMessage)
		{
		}
	}
}
