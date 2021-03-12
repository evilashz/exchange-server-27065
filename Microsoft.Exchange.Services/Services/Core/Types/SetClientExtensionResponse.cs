using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000554 RID: 1364
	[XmlType(TypeName = "SetClientExtensionResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetClientExtensionResponse : BaseResponseMessage
	{
		// Token: 0x0600265D RID: 9821 RVA: 0x000A65AA File Offset: 0x000A47AA
		public SetClientExtensionResponse() : base(ResponseType.SetClientExtensionResponseMessage)
		{
		}
	}
}
