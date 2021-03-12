using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000558 RID: 1368
	[XmlType("SetImGroupResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetImGroupResponseMessage : ResponseMessage
	{
		// Token: 0x0600266A RID: 9834 RVA: 0x000A6624 File Offset: 0x000A4824
		public SetImGroupResponseMessage()
		{
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000A662C File Offset: 0x000A482C
		internal SetImGroupResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000A6636 File Offset: 0x000A4836
		public override ResponseType GetResponseType()
		{
			return ResponseType.SetImGroupResponseMessage;
		}
	}
}
