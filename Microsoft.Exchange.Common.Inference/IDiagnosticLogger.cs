using System;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x02000009 RID: 9
	public interface IDiagnosticLogger : IDisposable
	{
		// Token: 0x06000045 RID: 69
		void LogError(string format, params object[] arguments);

		// Token: 0x06000046 RID: 70
		void LogInformation(string format, params object[] arguments);

		// Token: 0x06000047 RID: 71
		void LogDebug(string format, params object[] arguments);
	}
}
