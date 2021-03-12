using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E2 RID: 482
	[Serializable]
	public sealed class EsentBadColumnIdException : EsentUsageException
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x0001354A File Offset: 0x0001174A
		public EsentBadColumnIdException() : base("Column Id Incorrect", JET_err.BadColumnId)
		{
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001355C File Offset: 0x0001175C
		private EsentBadColumnIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
