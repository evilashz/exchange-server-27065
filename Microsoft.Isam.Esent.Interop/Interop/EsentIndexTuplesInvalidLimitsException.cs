using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D1 RID: 465
	[Serializable]
	public sealed class EsentIndexTuplesInvalidLimitsException : EsentUsageException
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x0001336E File Offset: 0x0001156E
		public EsentIndexTuplesInvalidLimitsException() : base("invalid min/max tuple length or max characters to index specified", JET_err.IndexTuplesInvalidLimits)
		{
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00013380 File Offset: 0x00011580
		private EsentIndexTuplesInvalidLimitsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
