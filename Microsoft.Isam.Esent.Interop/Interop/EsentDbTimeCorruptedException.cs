using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	public sealed class EsentDbTimeCorruptedException : EsentCorruptionException
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x000116AA File Offset: 0x0000F8AA
		public EsentDbTimeCorruptedException() : base("Dbtime on current page is greater than global database dbtime", JET_err.DbTimeCorrupted)
		{
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000116BC File Offset: 0x0000F8BC
		private EsentDbTimeCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
