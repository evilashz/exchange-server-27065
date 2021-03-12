using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface ILog
	{
		// Token: 0x06000021 RID: 33
		bool IsEnabled(LogLevel level);

		// Token: 0x06000022 RID: 34
		void Trace(string formatString, params object[] args);

		// Token: 0x06000023 RID: 35
		void Debug(string formatString, params object[] args);

		// Token: 0x06000024 RID: 36
		void Info(string formatString, params object[] args);

		// Token: 0x06000025 RID: 37
		void Warn(string formatString, params object[] args);

		// Token: 0x06000026 RID: 38
		void Error(string formatString, params object[] args);

		// Token: 0x06000027 RID: 39
		void Fatal(string formatString, params object[] args);

		// Token: 0x06000028 RID: 40
		void Fatal(Exception exception, string message = "");

		// Token: 0x06000029 RID: 41
		void Assert(bool condition, string formatString, params object[] args);

		// Token: 0x0600002A RID: 42
		void RetailAssert(bool condition, string formatString, params object[] args);
	}
}
