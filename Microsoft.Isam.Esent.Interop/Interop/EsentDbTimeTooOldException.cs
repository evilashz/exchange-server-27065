using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200010C RID: 268
	[Serializable]
	public sealed class EsentDbTimeTooOldException : EsentInconsistentException
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00011DE2 File Offset: 0x0000FFE2
		public EsentDbTimeTooOldException() : base("dbtime on page smaller than dbtimeBefore in record", JET_err.DbTimeTooOld)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		private EsentDbTimeTooOldException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
