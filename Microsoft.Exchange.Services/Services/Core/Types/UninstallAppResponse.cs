using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200056D RID: 1389
	[XmlType("UninstallAppResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UninstallAppResponse : ResponseMessage
	{
		// Token: 0x060026E1 RID: 9953 RVA: 0x000A6B82 File Offset: 0x000A4D82
		public UninstallAppResponse()
		{
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000A6B8A File Offset: 0x000A4D8A
		internal UninstallAppResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000A6B94 File Offset: 0x000A4D94
		public override ResponseType GetResponseType()
		{
			return ResponseType.UninstallAppResponseMessage;
		}
	}
}
