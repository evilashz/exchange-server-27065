using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200017D RID: 381
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OlcConfig : ConfigBase<OlcConfigSchema>
	{
		// Token: 0x04000819 RID: 2073
		public const string OlcTopology = "OlcTopology";
	}
}
