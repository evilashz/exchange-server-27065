using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C4 RID: 1220
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CreateUserConfigurationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateUserConfigurationResponse : BaseResponseMessage
	{
		// Token: 0x06002408 RID: 9224 RVA: 0x000A46DA File Offset: 0x000A28DA
		public CreateUserConfigurationResponse() : base(ResponseType.CreateUserConfigurationResponseMessage)
		{
		}
	}
}
