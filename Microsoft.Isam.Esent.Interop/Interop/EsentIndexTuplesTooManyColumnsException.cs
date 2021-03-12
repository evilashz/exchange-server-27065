using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001CD RID: 461
	[Serializable]
	public sealed class EsentIndexTuplesTooManyColumnsException : EsentObsoleteException
	{
		// Token: 0x060009AD RID: 2477 RVA: 0x000132FE File Offset: 0x000114FE
		public EsentIndexTuplesTooManyColumnsException() : base("tuple index may only have eleven columns in the index", JET_err.IndexTuplesTooManyColumns)
		{
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00013310 File Offset: 0x00011510
		private EsentIndexTuplesTooManyColumnsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
