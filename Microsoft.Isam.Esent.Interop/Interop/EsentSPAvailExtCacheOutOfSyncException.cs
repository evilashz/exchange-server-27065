using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	public sealed class EsentSPAvailExtCacheOutOfSyncException : EsentObsoleteException
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x0001163A File Offset: 0x0000F83A
		public EsentSPAvailExtCacheOutOfSyncException() : base("AvailExt cache doesn't match btree", JET_err.SPAvailExtCacheOutOfSync)
		{
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001164C File Offset: 0x0000F84C
		private EsentSPAvailExtCacheOutOfSyncException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
