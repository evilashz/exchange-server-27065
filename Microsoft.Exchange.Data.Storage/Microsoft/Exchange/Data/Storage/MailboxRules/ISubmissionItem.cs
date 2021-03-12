using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BF6 RID: 3062
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISubmissionItem : IDisposable
	{
		// Token: 0x17001DC7 RID: 7623
		// (get) Token: 0x06006D37 RID: 27959
		string SourceServerFqdn { get; }

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x06006D38 RID: 27960
		IPAddress SourceServerNetworkAddress { get; }

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x06006D39 RID: 27961
		DateTime OriginalCreateTime { get; }

		// Token: 0x06006D3A RID: 27962
		void Submit();

		// Token: 0x06006D3B RID: 27963
		void Submit(ProxyAddress sender, IEnumerable<Participant> recipients);
	}
}
