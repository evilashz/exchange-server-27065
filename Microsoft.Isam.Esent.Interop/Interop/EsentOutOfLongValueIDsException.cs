using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public sealed class EsentOutOfLongValueIDsException : EsentFragmentationException
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x0001274A File Offset: 0x0001094A
		public EsentOutOfLongValueIDsException() : base("Long-value ID counter has reached maximum value. (perform offline defrag to reclaim free/unused LongValueIDs)", JET_err.OutOfLongValueIDs)
		{
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001275C File Offset: 0x0001095C
		private EsentOutOfLongValueIDsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
