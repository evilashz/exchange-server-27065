using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon.FaultInjection
{
	// Token: 0x02000078 RID: 120
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MobileSpeechRecoFaultInjection
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x0000F205 File Offset: 0x0000D405
		internal static bool TryCreateException(string exceptionType, ref Exception exception)
		{
			return false;
		}

		// Token: 0x040002E8 RID: 744
		internal const uint MobileSpeechRecoRecognitionDelay = 4112919869U;
	}
}
