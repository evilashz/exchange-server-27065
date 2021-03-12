using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000005 RID: 5
	public interface ILogger : IDisposable
	{
		// Token: 0x06000012 RID: 18
		void Log(LocalizedString text);

		// Token: 0x06000013 RID: 19
		void Log(Exception e);

		// Token: 0x06000014 RID: 20
		void LogError(string text);

		// Token: 0x06000015 RID: 21
		void LogWarning(string text);

		// Token: 0x06000016 RID: 22
		void LogInformation(string text);
	}
}
