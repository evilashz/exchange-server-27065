using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004CE RID: 1230
	[XmlType("DisableAppResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class DisableAppResponse : ResponseMessage
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x000A47EF File Offset: 0x000A29EF
		public DisableAppResponse()
		{
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000A47F7 File Offset: 0x000A29F7
		internal DisableAppResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000A4801 File Offset: 0x000A2A01
		public override ResponseType GetResponseType()
		{
			return ResponseType.DisableAppResponseMessage;
		}
	}
}
