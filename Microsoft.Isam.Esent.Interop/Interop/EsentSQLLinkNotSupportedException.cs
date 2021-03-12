using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000147 RID: 327
	[Serializable]
	public sealed class EsentSQLLinkNotSupportedException : EsentObsoleteException
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x00012456 File Offset: 0x00010656
		public EsentSQLLinkNotSupportedException() : base("SQL Link support unavailable", JET_err.SQLLinkNotSupported)
		{
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00012468 File Offset: 0x00010668
		private EsentSQLLinkNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
