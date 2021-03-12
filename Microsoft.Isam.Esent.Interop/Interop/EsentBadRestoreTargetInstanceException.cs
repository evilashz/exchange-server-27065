using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public sealed class EsentBadRestoreTargetInstanceException : EsentUsageException
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00011EC2 File Offset: 0x000100C2
		public EsentBadRestoreTargetInstanceException() : base("TargetInstance specified for restore is not found or log files don't match", JET_err.BadRestoreTargetInstance)
		{
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00011ED4 File Offset: 0x000100D4
		private EsentBadRestoreTargetInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
