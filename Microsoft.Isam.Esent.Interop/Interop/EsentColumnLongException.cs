using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D4 RID: 468
	[Serializable]
	public sealed class EsentColumnLongException : EsentObsoleteException
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x000133C2 File Offset: 0x000115C2
		public EsentColumnLongException() : base("Column value is long", JET_err.ColumnLong)
		{
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000133D4 File Offset: 0x000115D4
		private EsentColumnLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
