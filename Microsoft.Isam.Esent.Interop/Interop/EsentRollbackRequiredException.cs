using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000177 RID: 375
	[Serializable]
	public sealed class EsentRollbackRequiredException : EsentObsoleteException
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x00012996 File Offset: 0x00010B96
		public EsentRollbackRequiredException() : base("Must rollback current transaction -- cannot commit or begin a new one", JET_err.RollbackRequired)
		{
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000129A8 File Offset: 0x00010BA8
		private EsentRollbackRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
