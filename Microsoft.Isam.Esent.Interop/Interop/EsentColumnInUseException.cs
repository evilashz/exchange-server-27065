using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public sealed class EsentColumnInUseException : EsentUsageException
	{
		// Token: 0x060008AD RID: 2221 RVA: 0x000124FE File Offset: 0x000106FE
		public EsentColumnInUseException() : base("Column used in an index", JET_err.ColumnInUse)
		{
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00012510 File Offset: 0x00010710
		private EsentColumnInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
