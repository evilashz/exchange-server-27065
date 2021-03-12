using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D8 RID: 472
	[Serializable]
	public sealed class EsentColumnIndexedException : EsentObsoleteException
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x00013432 File Offset: 0x00011632
		public EsentColumnIndexedException() : base("Column indexed, cannot delete", JET_err.ColumnIndexed)
		{
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00013444 File Offset: 0x00011644
		private EsentColumnIndexedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
