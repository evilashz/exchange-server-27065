using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200070A RID: 1802
	[ComVisible(true)]
	public interface ISurrogateSelector
	{
		// Token: 0x060050A4 RID: 20644
		[SecurityCritical]
		void ChainSelector(ISurrogateSelector selector);

		// Token: 0x060050A5 RID: 20645
		[SecurityCritical]
		ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);

		// Token: 0x060050A6 RID: 20646
		[SecurityCritical]
		ISurrogateSelector GetNextSelector();
	}
}
