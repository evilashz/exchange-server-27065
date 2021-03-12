using System;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000BA5 RID: 2981
	internal interface IServerDiagnosticsHandler
	{
		// Token: 0x06003FF0 RID: 16368
		void AnalyseException(ref Exception exception);

		// Token: 0x06003FF1 RID: 16369
		void LogException(Exception exception);

		// Token: 0x06003FF2 RID: 16370
		void LogMessage(string message);
	}
}
