using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	public sealed class EsentTooManyOpenDatabasesException : EsentObsoleteException
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x00012392 File Offset: 0x00010592
		public EsentTooManyOpenDatabasesException() : base("Too many open databases", JET_err.TooManyOpenDatabases)
		{
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000123A4 File Offset: 0x000105A4
		private EsentTooManyOpenDatabasesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
