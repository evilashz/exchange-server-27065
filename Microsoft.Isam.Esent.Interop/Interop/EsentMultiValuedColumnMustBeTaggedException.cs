using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001DC RID: 476
	[Serializable]
	public sealed class EsentMultiValuedColumnMustBeTaggedException : EsentUsageException
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x000134A2 File Offset: 0x000116A2
		public EsentMultiValuedColumnMustBeTaggedException() : base("Attempted to create a multi-valued column, but column was not Tagged", JET_err.MultiValuedColumnMustBeTagged)
		{
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000134B4 File Offset: 0x000116B4
		private EsentMultiValuedColumnMustBeTaggedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
