using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000166 RID: 358
	[Serializable]
	public sealed class EsentRunningInOneInstanceModeException : EsentUsageException
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x000127BA File Offset: 0x000109BA
		public EsentRunningInOneInstanceModeException() : base("Multi-instance call with single-instance mode enabled", JET_err.RunningInOneInstanceMode)
		{
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000127CC File Offset: 0x000109CC
		private EsentRunningInOneInstanceModeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
