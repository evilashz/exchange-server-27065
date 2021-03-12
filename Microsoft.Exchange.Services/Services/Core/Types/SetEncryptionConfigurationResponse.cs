using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000556 RID: 1366
	[XmlType(TypeName = "SetEncryptionConfigurationResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetEncryptionConfigurationResponse : ResponseMessage
	{
		// Token: 0x06002663 RID: 9827 RVA: 0x000A65E1 File Offset: 0x000A47E1
		public SetEncryptionConfigurationResponse()
		{
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000A65E9 File Offset: 0x000A47E9
		internal SetEncryptionConfigurationResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000A65F3 File Offset: 0x000A47F3
		public override ResponseType GetResponseType()
		{
			return ResponseType.SetEncryptionConfigurationResponseMessage;
		}
	}
}
