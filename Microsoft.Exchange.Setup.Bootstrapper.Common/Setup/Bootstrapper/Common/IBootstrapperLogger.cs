using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000002 RID: 2
	public interface IBootstrapperLogger
	{
		// Token: 0x06000001 RID: 1
		void StartLogging();

		// Token: 0x06000002 RID: 2
		void StopLogging();

		// Token: 0x06000003 RID: 3
		void Log(LocalizedString localizedString);

		// Token: 0x06000004 RID: 4
		void LogWarning(LocalizedString localizedString);

		// Token: 0x06000005 RID: 5
		void LogError(Exception e);

		// Token: 0x06000006 RID: 6
		void IncreaseIndentation(LocalizedString tag);

		// Token: 0x06000007 RID: 7
		void DecreaseIndentation();
	}
}
