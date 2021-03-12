using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	public sealed class EsentIndexCantBuildException : EsentObsoleteException
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x00013176 File Offset: 0x00011376
		public EsentIndexCantBuildException() : base("Index build failed", JET_err.IndexCantBuild)
		{
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00013188 File Offset: 0x00011388
		private EsentIndexCantBuildException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
