using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public sealed class EsentInvalidDatabaseIdException : EsentUsageException
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x000121B6 File Offset: 0x000103B6
		public EsentInvalidDatabaseIdException() : base("Invalid database id", JET_err.InvalidDatabaseId)
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000121C8 File Offset: 0x000103C8
		private EsentInvalidDatabaseIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
