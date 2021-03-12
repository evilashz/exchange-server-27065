using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	public sealed class EsentNoCurrentIndexException : EsentUsageException
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x00013512 File Offset: 0x00011712
		public EsentNoCurrentIndexException() : base("Invalid w/o a current index", JET_err.NoCurrentIndex)
		{
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00013524 File Offset: 0x00011724
		private EsentNoCurrentIndexException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
