using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	public sealed class EsentRunningInMultiInstanceModeException : EsentUsageException
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x000127D6 File Offset: 0x000109D6
		public EsentRunningInMultiInstanceModeException() : base("Single-instance call with multi-instance mode enabled", JET_err.RunningInMultiInstanceMode)
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000127E8 File Offset: 0x000109E8
		private EsentRunningInMultiInstanceModeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
