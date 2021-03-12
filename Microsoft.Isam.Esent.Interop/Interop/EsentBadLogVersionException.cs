using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	public sealed class EsentBadLogVersionException : EsentInconsistentException
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x000118DA File Offset: 0x0000FADA
		public EsentBadLogVersionException() : base("Version of log file is not compatible with Jet version", JET_err.BadLogVersion)
		{
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000118EC File Offset: 0x0000FAEC
		private EsentBadLogVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
