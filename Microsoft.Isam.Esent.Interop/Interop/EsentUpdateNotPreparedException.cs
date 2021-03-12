using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F4 RID: 500
	[Serializable]
	public sealed class EsentUpdateNotPreparedException : EsentUsageException
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x00013742 File Offset: 0x00011942
		public EsentUpdateNotPreparedException() : base("No call to JetPrepareUpdate", JET_err.UpdateNotPrepared)
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00013754 File Offset: 0x00011954
		private EsentUpdateNotPreparedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
