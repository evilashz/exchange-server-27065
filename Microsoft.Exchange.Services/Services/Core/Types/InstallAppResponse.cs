using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000528 RID: 1320
	[XmlType("InstallAppResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class InstallAppResponse : ResponseMessage
	{
		// Token: 0x060025CE RID: 9678 RVA: 0x000A5F7F File Offset: 0x000A417F
		public InstallAppResponse()
		{
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000A5F87 File Offset: 0x000A4187
		internal InstallAppResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000A5F91 File Offset: 0x000A4191
		public override ResponseType GetResponseType()
		{
			return ResponseType.InstallAppResponseMessage;
		}
	}
}
