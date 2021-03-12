using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface IPop3Connection : IConnection<IPop3Connection>, IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4
		Pop3ConnectionContext ConnectionContext { get; }

		// Token: 0x06000005 RID: 5
		void ConnectAndAuthenticate(Pop3ServerParameters serverParameters, Pop3AuthenticationParameters authenticationParameters);

		// Token: 0x06000006 RID: 6
		Pop3ResultData DeleteEmails(List<int> messageIds);

		// Token: 0x06000007 RID: 7
		Pop3ResultData GetEmail(int messageId);

		// Token: 0x06000008 RID: 8
		Pop3ResultData GetUniqueIds();

		// Token: 0x06000009 RID: 9
		Pop3ResultData Quit();

		// Token: 0x0600000A RID: 10
		Pop3ResultData VerifyAccount();
	}
}
