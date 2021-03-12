using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E7 RID: 487
	[Serializable]
	public sealed class EsentMultiValuedDuplicateException : EsentStateException
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x000135D6 File Offset: 0x000117D6
		public EsentMultiValuedDuplicateException() : base("Duplicate detected on a unique multi-valued column", JET_err.MultiValuedDuplicate)
		{
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000135E8 File Offset: 0x000117E8
		private EsentMultiValuedDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
