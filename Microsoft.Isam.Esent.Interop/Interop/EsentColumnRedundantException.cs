using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001DD RID: 477
	[Serializable]
	public sealed class EsentColumnRedundantException : EsentUsageException
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x000134BE File Offset: 0x000116BE
		public EsentColumnRedundantException() : base("Second autoincrement or version column", JET_err.ColumnRedundant)
		{
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000134D0 File Offset: 0x000116D0
		private EsentColumnRedundantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
