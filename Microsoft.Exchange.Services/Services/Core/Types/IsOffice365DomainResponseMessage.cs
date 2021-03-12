using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200052B RID: 1323
	[XmlType("IsOffice365DomainResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class IsOffice365DomainResponseMessage : ResponseMessage
	{
		// Token: 0x060025D8 RID: 9688 RVA: 0x000A5FD5 File Offset: 0x000A41D5
		public IsOffice365DomainResponseMessage()
		{
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000A5FDD File Offset: 0x000A41DD
		internal IsOffice365DomainResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
