using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C2 RID: 1218
	[XmlType("CreateUnifiedMailboxResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateUnifiedMailboxResponse : ResponseMessage
	{
		// Token: 0x06002401 RID: 9217 RVA: 0x000A469B File Offset: 0x000A289B
		public CreateUnifiedMailboxResponse()
		{
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000A46A3 File Offset: 0x000A28A3
		internal CreateUnifiedMailboxResponse(ServiceResultCode code, ServiceError error, string userPrincipalName) : base(code, error)
		{
			this.UserPrincipalName = userPrincipalName;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000A46B4 File Offset: 0x000A28B4
		public override ResponseType GetResponseType()
		{
			return ResponseType.CreateUnifiedMailboxResponseMessage;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x000A46B7 File Offset: 0x000A28B7
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x000A46BF File Offset: 0x000A28BF
		[XmlElement("UserPrincipalName", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string UserPrincipalName { get; set; }
	}
}
