using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	public sealed class EsentKeyIsMadeException : EsentUsageException
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x0001352E File Offset: 0x0001172E
		public EsentKeyIsMadeException() : base("The key is completely made", JET_err.KeyIsMade)
		{
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00013540 File Offset: 0x00011740
		private EsentKeyIsMadeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
