using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000216 RID: 534
	[Serializable]
	public sealed class EsentTooManyTestInjectionsException : EsentUsageException
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x00013AFA File Offset: 0x00011CFA
		public EsentTooManyTestInjectionsException() : base("Internal test injection limit hit", JET_err.TooManyTestInjections)
		{
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00013B0C File Offset: 0x00011D0C
		private EsentTooManyTestInjectionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
