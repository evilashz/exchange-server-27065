using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D3 RID: 467
	[Serializable]
	public sealed class EsentIndexTuplesKeyTooSmallException : EsentUsageException
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x000133A6 File Offset: 0x000115A6
		public EsentIndexTuplesKeyTooSmallException() : base("specified key does not meet minimum tuple length", JET_err.IndexTuplesKeyTooSmall)
		{
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000133B8 File Offset: 0x000115B8
		private EsentIndexTuplesKeyTooSmallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
