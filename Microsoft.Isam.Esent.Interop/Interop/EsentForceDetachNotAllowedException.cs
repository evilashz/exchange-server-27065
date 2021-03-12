using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	public sealed class EsentForceDetachNotAllowedException : EsentUsageException
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x00012DDA File Offset: 0x00010FDA
		public EsentForceDetachNotAllowedException() : base("Force Detach allowed only after normal detach errored out", JET_err.ForceDetachNotAllowed)
		{
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00012DEC File Offset: 0x00010FEC
		private EsentForceDetachNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
