using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200018E RID: 398
	[Serializable]
	public sealed class EsentDatabaseNotFoundException : EsentUsageException
	{
		// Token: 0x0600092F RID: 2351 RVA: 0x00012C1A File Offset: 0x00010E1A
		public EsentDatabaseNotFoundException() : base("No such database", JET_err.DatabaseNotFound)
		{
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00012C2C File Offset: 0x00010E2C
		private EsentDatabaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
