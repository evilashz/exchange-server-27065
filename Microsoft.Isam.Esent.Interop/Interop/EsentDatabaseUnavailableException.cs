using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200016F RID: 367
	[Serializable]
	public sealed class EsentDatabaseUnavailableException : EsentObsoleteException
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x000128B6 File Offset: 0x00010AB6
		public EsentDatabaseUnavailableException() : base("This database cannot be used because it encountered a fatal error", JET_err.DatabaseUnavailable)
		{
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000128C8 File Offset: 0x00010AC8
		private EsentDatabaseUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
