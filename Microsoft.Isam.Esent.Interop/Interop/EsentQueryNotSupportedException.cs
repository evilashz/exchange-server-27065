using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000146 RID: 326
	[Serializable]
	public sealed class EsentQueryNotSupportedException : EsentUsageException
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x0001243A File Offset: 0x0001063A
		public EsentQueryNotSupportedException() : base("Query support unavailable", JET_err.QueryNotSupported)
		{
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001244C File Offset: 0x0001064C
		private EsentQueryNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
