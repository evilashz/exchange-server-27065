using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000209 RID: 521
	[Serializable]
	public sealed class EsentSessionContextNotSetByThisThreadException : EsentUsageException
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x0001398E File Offset: 0x00011B8E
		public EsentSessionContextNotSetByThisThreadException() : base("Tried to reset session context, but current thread did not orignally set the session context", JET_err.SessionContextNotSetByThisThread)
		{
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000139A0 File Offset: 0x00011BA0
		private EsentSessionContextNotSetByThisThreadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
