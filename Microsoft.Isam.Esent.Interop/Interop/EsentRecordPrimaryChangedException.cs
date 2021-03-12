using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F0 RID: 496
	[Serializable]
	public sealed class EsentRecordPrimaryChangedException : EsentUsageException
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x000136D2 File Offset: 0x000118D2
		public EsentRecordPrimaryChangedException() : base("Primary key may not change", JET_err.RecordPrimaryChanged)
		{
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000136E4 File Offset: 0x000118E4
		private EsentRecordPrimaryChangedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
