using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200015D RID: 349
	[Serializable]
	public sealed class EsentCurrencyStackOutOfMemoryException : EsentObsoleteException
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x000126BE File Offset: 0x000108BE
		public EsentCurrencyStackOutOfMemoryException() : base("UNUSED: lCSRPerfFUCB * g_lCursorsMax exceeded (XJET only)", JET_err.CurrencyStackOutOfMemory)
		{
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000126D0 File Offset: 0x000108D0
		private EsentCurrencyStackOutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
