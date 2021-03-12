using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000164 RID: 356
	[Serializable]
	public sealed class EsentOutOfDbtimeValuesException : EsentFragmentationException
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x00012782 File Offset: 0x00010982
		public EsentOutOfDbtimeValuesException() : base("Dbtime counter has reached maximum value (perform offline defrag to reclaim free/unused Dbtime values)", JET_err.OutOfDbtimeValues)
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00012794 File Offset: 0x00010994
		private EsentOutOfDbtimeValuesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
