using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A3 RID: 419
	[Serializable]
	public sealed class EsentInvalidCreateDbVersionException : EsentInconsistentException
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x00012E66 File Offset: 0x00011066
		public EsentInvalidCreateDbVersionException() : base("recovery tried to replay a database creation, but the database was originally created with an incompatible (likely older) version of the database engine", JET_err.InvalidCreateDbVersion)
		{
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00012E78 File Offset: 0x00011078
		private EsentInvalidCreateDbVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
