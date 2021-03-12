using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001FA RID: 506
	[Serializable]
	public sealed class EsentInvalidOnSortException : EsentObsoleteException
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x000137EA File Offset: 0x000119EA
		public EsentInvalidOnSortException() : base("Invalid operation on Sort", JET_err.InvalidOnSort)
		{
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000137FC File Offset: 0x000119FC
		private EsentInvalidOnSortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
