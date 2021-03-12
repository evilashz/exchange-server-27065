using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	public sealed class EsentKeyTooBigException : EsentObsoleteException
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x0001171A File Offset: 0x0000F91A
		public EsentKeyTooBigException() : base("Key is too large", JET_err.KeyTooBig)
		{
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001172C File Offset: 0x0000F92C
		private EsentKeyTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
