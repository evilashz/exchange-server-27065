using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200053E RID: 1342
	[XmlType(TypeName = "RemoveDelegateResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class RemoveDelegateResponseMessage : DelegateResponseMessage
	{
		// Token: 0x06002625 RID: 9765 RVA: 0x000A636B File Offset: 0x000A456B
		public RemoveDelegateResponseMessage()
		{
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000A6373 File Offset: 0x000A4573
		internal RemoveDelegateResponseMessage(ServiceResultCode code, ServiceError error, DelegateUserResponseMessageType[] delegateUsers) : base(code, error, delegateUsers, ResponseType.RemoveDelegateResponseMessage)
		{
		}
	}
}
