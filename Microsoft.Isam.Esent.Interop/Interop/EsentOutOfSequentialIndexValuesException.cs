using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000165 RID: 357
	[Serializable]
	public sealed class EsentOutOfSequentialIndexValuesException : EsentFragmentationException
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0001279E File Offset: 0x0001099E
		public EsentOutOfSequentialIndexValuesException() : base("Sequential index counter has reached maximum value (perform offline defrag to reclaim free/unused SequentialIndex values)", JET_err.OutOfSequentialIndexValues)
		{
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000127B0 File Offset: 0x000109B0
		private EsentOutOfSequentialIndexValuesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
