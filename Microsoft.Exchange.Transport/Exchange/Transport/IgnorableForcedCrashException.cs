using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	internal sealed class IgnorableForcedCrashException : ApplicationException
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000F741 File Offset: 0x0000D941
		public IgnorableForcedCrashException() : base("Crashing the process to generate a crash dump. Please ignore this crash.")
		{
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000F74E File Offset: 0x0000D94E
		public IgnorableForcedCrashException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040001E0 RID: 480
		private const string ForcedCrashExceptionMessage = "Crashing the process to generate a crash dump. Please ignore this crash.";
	}
}
