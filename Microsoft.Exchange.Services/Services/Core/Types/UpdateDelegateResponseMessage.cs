using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000572 RID: 1394
	[XmlType(TypeName = "UpdateDelegateResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class UpdateDelegateResponseMessage : DelegateResponseMessage
	{
		// Token: 0x060026ED RID: 9965 RVA: 0x000A6BE3 File Offset: 0x000A4DE3
		public UpdateDelegateResponseMessage()
		{
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000A6BEB File Offset: 0x000A4DEB
		internal UpdateDelegateResponseMessage(ServiceResultCode code, ServiceError error, DelegateUserResponseMessageType[] delegateUsers) : base(code, error, delegateUsers, ResponseType.UpdateDelegateResponseMessage)
		{
		}
	}
}
