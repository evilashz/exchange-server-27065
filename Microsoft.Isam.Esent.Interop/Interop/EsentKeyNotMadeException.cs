using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F3 RID: 499
	[Serializable]
	public sealed class EsentKeyNotMadeException : EsentUsageException
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x00013726 File Offset: 0x00011926
		public EsentKeyNotMadeException() : base("No call to JetMakeKey", JET_err.KeyNotMade)
		{
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00013738 File Offset: 0x00011938
		private EsentKeyNotMadeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
