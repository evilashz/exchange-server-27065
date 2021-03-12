using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000494 RID: 1172
	[Serializable]
	public class LegacyGatewayConnector : MailGateway
	{
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000D2329 File Offset: 0x000D0529
		internal override ADObjectSchema Schema
		{
			get
			{
				return LegacyGatewayConnector.schema;
			}
		}

		// Token: 0x04002416 RID: 9238
		private static LegacyGatewayConnectorSchema schema = ObjectSchema.GetInstance<LegacyGatewayConnectorSchema>();
	}
}
