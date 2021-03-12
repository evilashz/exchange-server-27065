using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery
{
	// Token: 0x0200019B RID: 411
	internal interface IConnectionSettingsReadProvider
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F9B RID: 3995
		string SourceId { get; }

		// Token: 0x06000F9C RID: 3996
		IEnumerable<ConnectionSettings> GetConnectionSettingsMatchingEmail(SmtpAddress email);
	}
}
