using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000093 RID: 147
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IGroupsLogger
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000388 RID: 904
		// (set) Token: 0x06000389 RID: 905
		Enum CurrentAction { get; set; }

		// Token: 0x0600038A RID: 906
		void LogTrace(string formatString, params object[] args);

		// Token: 0x0600038B RID: 907
		void LogException(Exception exception, string formatString, params object[] args);
	}
}
