using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	public sealed class EsentOutOfObjectIDsException : EsentFragmentationException
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x0001272E File Offset: 0x0001092E
		public EsentOutOfObjectIDsException() : base("Out of btree ObjectIDs (perform offline defrag to reclaim freed/unused ObjectIds)", JET_err.OutOfObjectIDs)
		{
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00012740 File Offset: 0x00010940
		private EsentOutOfObjectIDsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
