using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001DB RID: 475
	[Serializable]
	public sealed class EsentColumnDuplicateException : EsentUsageException
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x00013486 File Offset: 0x00011686
		public EsentColumnDuplicateException() : base("Field is already defined", JET_err.ColumnDuplicate)
		{
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00013498 File Offset: 0x00011698
		private EsentColumnDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
