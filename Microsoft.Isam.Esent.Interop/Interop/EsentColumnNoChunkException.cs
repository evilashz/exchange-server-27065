using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D5 RID: 469
	[Serializable]
	public sealed class EsentColumnNoChunkException : EsentUsageException
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x000133DE File Offset: 0x000115DE
		public EsentColumnNoChunkException() : base("No such chunk in long value", JET_err.ColumnNoChunk)
		{
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000133F0 File Offset: 0x000115F0
		private EsentColumnNoChunkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
