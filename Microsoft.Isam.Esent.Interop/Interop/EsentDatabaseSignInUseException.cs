using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public sealed class EsentDatabaseSignInUseException : EsentUsageException
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x00012E2E File Offset: 0x0001102E
		public EsentDatabaseSignInUseException() : base("Database with same signature in use", JET_err.DatabaseSignInUse)
		{
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00012E40 File Offset: 0x00011040
		private EsentDatabaseSignInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
