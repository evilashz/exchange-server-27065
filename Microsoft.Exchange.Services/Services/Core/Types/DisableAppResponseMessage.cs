using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004CF RID: 1231
	[XmlType("DisableAppResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class DisableAppResponseMessage : ResponseMessage
	{
		// Token: 0x06002423 RID: 9251 RVA: 0x000A4805 File Offset: 0x000A2A05
		public DisableAppResponseMessage()
		{
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000A480D File Offset: 0x000A2A0D
		internal DisableAppResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
