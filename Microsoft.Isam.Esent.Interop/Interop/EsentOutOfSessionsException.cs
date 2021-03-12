using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000171 RID: 369
	[Serializable]
	public sealed class EsentOutOfSessionsException : EsentMemoryException
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x000128EE File Offset: 0x00010AEE
		public EsentOutOfSessionsException() : base("Out of sessions", JET_err.OutOfSessions)
		{
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00012900 File Offset: 0x00010B00
		private EsentOutOfSessionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
