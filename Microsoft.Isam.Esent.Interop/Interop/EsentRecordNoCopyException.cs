using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001EE RID: 494
	[Serializable]
	public sealed class EsentRecordNoCopyException : EsentUsageException
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x0001369A File Offset: 0x0001189A
		public EsentRecordNoCopyException() : base("No working buffer", JET_err.RecordNoCopy)
		{
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000136AC File Offset: 0x000118AC
		private EsentRecordNoCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
