using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000DD RID: 221
	[Serializable]
	public sealed class EsentLogGenerationMismatchException : EsentInconsistentException
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x000118BE File Offset: 0x0000FABE
		public EsentLogGenerationMismatchException() : base("Name of logfile does not match internal generation number", JET_err.LogGenerationMismatch)
		{
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000118D0 File Offset: 0x0000FAD0
		private EsentLogGenerationMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
