using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004CC RID: 1228
	[XmlType("DeleteUserConfigurationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteUserConfigurationResponse : BaseResponseMessage
	{
		// Token: 0x0600241E RID: 9246 RVA: 0x000A47D8 File Offset: 0x000A29D8
		public DeleteUserConfigurationResponse() : base(ResponseType.DeleteUserConfigurationResponseMessage)
		{
		}
	}
}
