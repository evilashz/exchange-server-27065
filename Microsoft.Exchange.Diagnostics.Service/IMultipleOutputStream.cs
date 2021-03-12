using System;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000007 RID: 7
	public interface IMultipleOutputStream
	{
		// Token: 0x06000021 RID: 33
		OutputStream OpenOutputStream(string analyzerName, string outputFormatName, string streamName);
	}
}
