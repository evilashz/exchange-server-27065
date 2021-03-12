using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200056E RID: 1390
	[XmlType("UninstallAppResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UninstallAppResponseMessage : ResponseMessage
	{
		// Token: 0x060026E4 RID: 9956 RVA: 0x000A6B98 File Offset: 0x000A4D98
		public UninstallAppResponseMessage()
		{
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000A6BA0 File Offset: 0x000A4DA0
		internal UninstallAppResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
