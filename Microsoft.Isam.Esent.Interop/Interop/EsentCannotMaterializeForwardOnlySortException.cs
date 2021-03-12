using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200017B RID: 379
	[Serializable]
	public sealed class EsentCannotMaterializeForwardOnlySortException : EsentUsageException
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x00012A06 File Offset: 0x00010C06
		public EsentCannotMaterializeForwardOnlySortException() : base("The temp table could not be created due to parameters that conflict with JET_bitTTForwardOnly", JET_err.CannotMaterializeForwardOnlySort)
		{
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00012A18 File Offset: 0x00010C18
		private EsentCannotMaterializeForwardOnlySortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
