using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000206 RID: 518
	[Serializable]
	public sealed class EsentSessionSharingViolationException : EsentUsageException
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x0001393A File Offset: 0x00011B3A
		public EsentSessionSharingViolationException() : base("Multiple threads are using the same session", JET_err.SessionSharingViolation)
		{
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001394C File Offset: 0x00011B4C
		private EsentSessionSharingViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
