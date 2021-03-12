using System;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery
{
	// Token: 0x0200019C RID: 412
	internal interface IConnectionSettingsWriteProvider
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F9D RID: 3997
		string SourceId { get; }

		// Token: 0x06000F9E RID: 3998
		bool SetConnectionSettingsMatchingEmail(SmtpAddress email, ConnectionSettings connectionSettings);
	}
}
