using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000123 RID: 291
	[Serializable]
	public sealed class EsentCheckpointDepthTooDeepException : EsentQuotaException
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x00012066 File Offset: 0x00010266
		public EsentCheckpointDepthTooDeepException() : base("too many outstanding generations between checkpoint and current generation", JET_err.CheckpointDepthTooDeep)
		{
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00012078 File Offset: 0x00010278
		private EsentCheckpointDepthTooDeepException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
