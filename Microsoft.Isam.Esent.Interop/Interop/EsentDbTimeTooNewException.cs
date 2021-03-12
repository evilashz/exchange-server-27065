using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	public sealed class EsentDbTimeTooNewException : EsentInconsistentException
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x00011DFE File Offset: 0x0000FFFE
		public EsentDbTimeTooNewException() : base("dbtime on page in advance of the dbtimeBefore in record", JET_err.DbTimeTooNew)
		{
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00011E10 File Offset: 0x00010010
		private EsentDbTimeTooNewException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
