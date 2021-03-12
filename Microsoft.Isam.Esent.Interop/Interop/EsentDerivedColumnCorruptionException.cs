using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001EA RID: 490
	[Serializable]
	public sealed class EsentDerivedColumnCorruptionException : EsentCorruptionException
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x0001362A File Offset: 0x0001182A
		public EsentDerivedColumnCorruptionException() : base("Invalid column in derived table", JET_err.DerivedColumnCorruption)
		{
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001363C File Offset: 0x0001183C
		private EsentDerivedColumnCorruptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
