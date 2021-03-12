using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000522 RID: 1314
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetUserConfigurationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUserConfigurationResponseMessage : ResponseMessage
	{
		// Token: 0x060025B2 RID: 9650 RVA: 0x000A5DC5 File Offset: 0x000A3FC5
		public GetUserConfigurationResponseMessage()
		{
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000A5DCD File Offset: 0x000A3FCD
		internal GetUserConfigurationResponseMessage(ServiceResultCode code, ServiceError error, ServiceUserConfiguration serviceUserConfiguration) : base(code, error)
		{
			this.UserConfiguration = serviceUserConfiguration;
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x000A5DDE File Offset: 0x000A3FDE
		// (set) Token: 0x060025B5 RID: 9653 RVA: 0x000A5DE6 File Offset: 0x000A3FE6
		[XmlElement("UserConfiguration")]
		[DataMember(Name = "UserConfiguration", EmitDefaultValue = false)]
		public ServiceUserConfiguration UserConfiguration { get; set; }
	}
}
