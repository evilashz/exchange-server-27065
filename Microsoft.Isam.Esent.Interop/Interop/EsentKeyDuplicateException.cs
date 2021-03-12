using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F1 RID: 497
	[Serializable]
	public sealed class EsentKeyDuplicateException : EsentStateException
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x000136EE File Offset: 0x000118EE
		public EsentKeyDuplicateException() : base("Illegal duplicate key", JET_err.KeyDuplicate)
		{
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00013700 File Offset: 0x00011900
		private EsentKeyDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
