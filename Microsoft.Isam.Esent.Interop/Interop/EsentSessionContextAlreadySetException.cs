using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	public sealed class EsentSessionContextAlreadySetException : EsentUsageException
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x00013972 File Offset: 0x00011B72
		public EsentSessionContextAlreadySetException() : base("Specified session already has a session context set", JET_err.SessionContextAlreadySet)
		{
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00013984 File Offset: 0x00011B84
		private EsentSessionContextAlreadySetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
