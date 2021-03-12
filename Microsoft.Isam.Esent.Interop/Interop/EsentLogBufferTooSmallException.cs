using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	public sealed class EsentLogBufferTooSmallException : EsentObsoleteException
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x0001192E File Offset: 0x0000FB2E
		public EsentLogBufferTooSmallException() : base("Log buffer is too small for recovery", JET_err.LogBufferTooSmall)
		{
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00011940 File Offset: 0x0000FB40
		private EsentLogBufferTooSmallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
