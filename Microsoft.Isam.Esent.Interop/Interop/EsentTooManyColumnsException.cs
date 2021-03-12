using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000149 RID: 329
	[Serializable]
	public sealed class EsentTooManyColumnsException : EsentUsageException
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x0001248E File Offset: 0x0001068E
		public EsentTooManyColumnsException() : base("Too many columns defined", JET_err.TooManyColumns)
		{
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000124A0 File Offset: 0x000106A0
		private EsentTooManyColumnsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
