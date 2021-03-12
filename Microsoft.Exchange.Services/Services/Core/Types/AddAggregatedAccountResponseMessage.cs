using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A4 RID: 1188
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("AddAggregatedAccountResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddAggregatedAccountResponseMessage : ResponseMessage
	{
		// Token: 0x0600239C RID: 9116 RVA: 0x000A4165 File Offset: 0x000A2365
		public AddAggregatedAccountResponseMessage()
		{
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000A416D File Offset: 0x000A236D
		internal AddAggregatedAccountResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
