using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A5 RID: 1189
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public abstract class DelegateResponseMessage : ResponseMessage
	{
		// Token: 0x0600239E RID: 9118 RVA: 0x000A4177 File Offset: 0x000A2377
		public DelegateResponseMessage()
		{
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000A417F File Offset: 0x000A237F
		internal DelegateResponseMessage(ServiceResultCode code, ServiceError error, DelegateUserResponseMessageType[] delegateUsers, ResponseType responseType) : base(code, error)
		{
			this.delegateUsers = delegateUsers;
			this.responseType = responseType;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x000A4198 File Offset: 0x000A2398
		// (set) Token: 0x060023A1 RID: 9121 RVA: 0x000A41A0 File Offset: 0x000A23A0
		[XmlArrayItem("DelegateUserResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IsNullable = false)]
		public DelegateUserResponseMessageType[] ResponseMessages
		{
			get
			{
				return this.delegateUsers;
			}
			set
			{
				this.delegateUsers = value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x000A41A9 File Offset: 0x000A23A9
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x000A41B0 File Offset: 0x000A23B0
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				return ResponseMessage.namespaces;
			}
			set
			{
			}
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000A41B2 File Offset: 0x000A23B2
		public override ResponseType GetResponseType()
		{
			return this.responseType;
		}

		// Token: 0x04001556 RID: 5462
		private ResponseType responseType;

		// Token: 0x04001557 RID: 5463
		private DelegateUserResponseMessageType[] delegateUsers;
	}
}
