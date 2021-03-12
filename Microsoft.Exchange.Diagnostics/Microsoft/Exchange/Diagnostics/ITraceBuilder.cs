using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200009E RID: 158
	public interface ITraceBuilder
	{
		// Token: 0x060003BE RID: 958
		void BeginEntry(TraceType traceType, Guid componentGuid, int traceTag, long id, string format);

		// Token: 0x060003BF RID: 959
		void EndEntry();

		// Token: 0x060003C0 RID: 960
		void AddArgument<T>(T value);

		// Token: 0x060003C1 RID: 961
		void AddArgument(int value);

		// Token: 0x060003C2 RID: 962
		void AddArgument(long value);

		// Token: 0x060003C3 RID: 963
		void AddArgument(Guid value);

		// Token: 0x060003C4 RID: 964
		void AddArgument(string value);

		// Token: 0x060003C5 RID: 965
		void AddArgument(char[] value);

		// Token: 0x060003C6 RID: 966
		unsafe void AddArgument(char* value, int length);
	}
}
