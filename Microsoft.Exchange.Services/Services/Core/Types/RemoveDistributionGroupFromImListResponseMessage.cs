using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200053F RID: 1343
	[XmlType("RemoveDistributionGroupFromImListResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveDistributionGroupFromImListResponseMessage : ResponseMessage
	{
		// Token: 0x06002627 RID: 9767 RVA: 0x000A6380 File Offset: 0x000A4580
		public RemoveDistributionGroupFromImListResponseMessage()
		{
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000A6388 File Offset: 0x000A4588
		internal RemoveDistributionGroupFromImListResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000A6392 File Offset: 0x000A4592
		public override ResponseType GetResponseType()
		{
			return ResponseType.RemoveDistributionGroupFromImListResponseMessage;
		}
	}
}
