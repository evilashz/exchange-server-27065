using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000529 RID: 1321
	[XmlType("InstallAppResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class InstallAppResponseMessage : ResponseMessage
	{
		// Token: 0x060025D1 RID: 9681 RVA: 0x000A5F95 File Offset: 0x000A4195
		public InstallAppResponseMessage()
		{
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000A5F9D File Offset: 0x000A419D
		internal InstallAppResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
