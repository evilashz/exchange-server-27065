using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	public sealed class EsentSPAvailExtCacheOutOfMemoryException : EsentObsoleteException
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x00011672 File Offset: 0x0000F872
		public EsentSPAvailExtCacheOutOfMemoryException() : base("Out of memory allocating an AvailExt cache node", JET_err.SPAvailExtCacheOutOfMemory)
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00011684 File Offset: 0x0000F884
		private EsentSPAvailExtCacheOutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
