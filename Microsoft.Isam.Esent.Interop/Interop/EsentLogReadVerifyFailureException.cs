using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000122 RID: 290
	[Serializable]
	public sealed class EsentLogReadVerifyFailureException : EsentCorruptionException
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x0001204A File Offset: 0x0001024A
		public EsentLogReadVerifyFailureException() : base("Checksum error in log file during backup", JET_err.LogReadVerifyFailure)
		{
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001205C File Offset: 0x0001025C
		private EsentLogReadVerifyFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
