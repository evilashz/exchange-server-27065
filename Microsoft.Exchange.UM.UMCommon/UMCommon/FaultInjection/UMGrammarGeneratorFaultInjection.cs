using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon.FaultInjection
{
	// Token: 0x0200007A RID: 122
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMGrammarGeneratorFaultInjection
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x0000F21D File Offset: 0x0000D41D
		internal static bool TryCreateException(string exceptionType, ref Exception exception)
		{
			if (exceptionType != null && UMGrammarGeneratorFaultInjection.SpeechEngineException.Equals(exceptionType))
			{
				exception = new COMException(new LocalizedString("This is a test purpose exception for testing"));
				return true;
			}
			return false;
		}

		// Token: 0x040002EA RID: 746
		internal const uint SpeechGrammarCompile = 3039178045U;

		// Token: 0x040002EB RID: 747
		internal const uint SpeechEngineNormalize = 2502307133U;

		// Token: 0x040002EC RID: 748
		private static readonly string SpeechEngineException = "COMException";
	}
}
