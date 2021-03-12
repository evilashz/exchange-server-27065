using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000155 RID: 341
	[Serializable]
	public sealed class EsentTooManyActiveUsersException : EsentUsageException
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x000125DE File Offset: 0x000107DE
		public EsentTooManyActiveUsersException() : base("Too many active database users", JET_err.TooManyActiveUsers)
		{
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000125F0 File Offset: 0x000107F0
		private EsentTooManyActiveUsersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
