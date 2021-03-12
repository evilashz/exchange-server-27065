using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200057D RID: 1405
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("UpdateUserConfigurationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UpdateUserConfigurationResponse : BaseResponseMessage
	{
		// Token: 0x06002715 RID: 10005 RVA: 0x000A6E45 File Offset: 0x000A5045
		public UpdateUserConfigurationResponse() : base(ResponseType.UpdateUserConfigurationResponseMessage)
		{
		}
	}
}
