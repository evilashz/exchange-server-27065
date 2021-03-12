using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A6 RID: 1190
	[XmlType(TypeName = "AddDelegateResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class AddDelegateResponseMessage : DelegateResponseMessage
	{
		// Token: 0x060023A5 RID: 9125 RVA: 0x000A41BA File Offset: 0x000A23BA
		public AddDelegateResponseMessage()
		{
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x000A41C2 File Offset: 0x000A23C2
		internal AddDelegateResponseMessage(ServiceResultCode code, ServiceError error, DelegateUserResponseMessageType[] delegateUsers) : base(code, error, delegateUsers, ResponseType.AddDelegateResponseMessage)
		{
		}
	}
}
