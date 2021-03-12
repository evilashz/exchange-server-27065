using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.Partner
{
	// Token: 0x02000003 RID: 3
	internal abstract class ExtendedRoutingSmtpServer : SmtpServer
	{
		// Token: 0x06000002 RID: 2
		public abstract void TrackAgentInfo(string agentName, string groupName, List<KeyValuePair<string, string>> data);
	}
}
