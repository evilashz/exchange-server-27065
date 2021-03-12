using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000163 RID: 355
	[Serializable]
	public sealed class EsentOutOfAutoincrementValuesException : EsentFragmentationException
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00012766 File Offset: 0x00010966
		public EsentOutOfAutoincrementValuesException() : base("Auto-increment counter has reached maximum value (offline defrag WILL NOT be able to reclaim free/unused Auto-increment values).", JET_err.OutOfAutoincrementValues)
		{
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00012778 File Offset: 0x00010978
		private EsentOutOfAutoincrementValuesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
