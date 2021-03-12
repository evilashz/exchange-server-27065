using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000514 RID: 1300
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetServiceConfigurationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetServiceConfigurationResponseMessage : ResponseMessage
	{
		// Token: 0x06002561 RID: 9569 RVA: 0x000A59FB File Offset: 0x000A3BFB
		public GetServiceConfigurationResponseMessage()
		{
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000A5A03 File Offset: 0x000A3C03
		internal GetServiceConfigurationResponseMessage(ServiceResultCode code, ServiceError error, ServiceConfigurationResponseMessage[] responseMessages) : base(code, error)
		{
			this.responseMessageArray = responseMessages;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x000A5A14 File Offset: 0x000A3C14
		// (set) Token: 0x06002564 RID: 9572 RVA: 0x000A5A1C File Offset: 0x000A3C1C
		[XmlArrayItem("ServiceConfigurationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", IsNullable = false)]
		public ServiceConfigurationResponseMessage[] ResponseMessages
		{
			get
			{
				return this.responseMessageArray;
			}
			set
			{
				this.responseMessageArray = value;
			}
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000A5A25 File Offset: 0x000A3C25
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetServiceConfigurationResponseMessage;
		}

		// Token: 0x040015BB RID: 5563
		private ServiceConfigurationResponseMessage[] responseMessageArray;
	}
}
