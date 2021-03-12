using System;
using System.Text;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C7 RID: 455
	internal static class MapiHttpOperationUtilities
	{
		// Token: 0x0600099E RID: 2462 RVA: 0x0001E6EC File Offset: 0x0001C8EC
		internal static Encoding GetStateEncodingOrDefault(NspiState state)
		{
			Encoding asciiEncoding;
			if (state == null || !String8Encodings.TryGetEncoding(state.CodePage, out asciiEncoding))
			{
				asciiEncoding = CTSGlobals.AsciiEncoding;
			}
			return asciiEncoding;
		}
	}
}
