using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001AF RID: 431
	[Serializable]
	public sealed class EsentTooManyOpenTablesException : EsentQuotaException
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00012FB6 File Offset: 0x000111B6
		public EsentTooManyOpenTablesException() : base("Cannot open any more tables (cleanup already attempted)", JET_err.TooManyOpenTables)
		{
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00012FC8 File Offset: 0x000111C8
		private EsentTooManyOpenTablesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
