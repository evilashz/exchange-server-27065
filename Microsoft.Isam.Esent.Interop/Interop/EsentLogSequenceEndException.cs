using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	public sealed class EsentLogSequenceEndException : EsentFragmentationException
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0001194A File Offset: 0x0000FB4A
		public EsentLogSequenceEndException() : base("Maximum log file number exceeded", JET_err.LogSequenceEnd)
		{
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001195C File Offset: 0x0000FB5C
		private EsentLogSequenceEndException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
