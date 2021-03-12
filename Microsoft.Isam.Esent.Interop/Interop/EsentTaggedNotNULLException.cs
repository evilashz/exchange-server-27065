using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001DF RID: 479
	[Serializable]
	public sealed class EsentTaggedNotNULLException : EsentObsoleteException
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x000134F6 File Offset: 0x000116F6
		public EsentTaggedNotNULLException() : base("No non-NULL tagged columns", JET_err.TaggedNotNULL)
		{
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00013508 File Offset: 0x00011708
		private EsentTaggedNotNULLException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
