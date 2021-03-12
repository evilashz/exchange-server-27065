using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F1 RID: 1777
	[Serializable]
	public sealed class DefaultToInternetSendConnector : ADPresentationObject
	{
		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x06005355 RID: 21333 RVA: 0x00130808 File Offset: 0x0012EA08
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return DefaultToInternetSendConnector.schema;
			}
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x06005356 RID: 21334 RVA: 0x0013080F File Offset: 0x0012EA0F
		public MultiValuedProperty<ADObjectId> SourceTransportServers
		{
			get
			{
				if (this.connector != null)
				{
					return this.connector.SourceTransportServers;
				}
				return null;
			}
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x00130826 File Offset: 0x0012EA26
		public DefaultToInternetSendConnector()
		{
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0013082E File Offset: 0x0012EA2E
		public DefaultToInternetSendConnector(SmtpSendConnectorConfig connector) : base(connector)
		{
			this.connector = connector;
		}

		// Token: 0x04003831 RID: 14385
		private static DefaultToInternetSendConnectorSchema schema = ObjectSchema.GetInstance<DefaultToInternetSendConnectorSchema>();

		// Token: 0x04003832 RID: 14386
		private SmtpSendConnectorConfig connector;
	}
}
