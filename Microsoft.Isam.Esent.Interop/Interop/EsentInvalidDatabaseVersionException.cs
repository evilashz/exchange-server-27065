using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	public sealed class EsentInvalidDatabaseVersionException : EsentInconsistentException
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x00012CC2 File Offset: 0x00010EC2
		public EsentInvalidDatabaseVersionException() : base("Database engine is incompatible with database", JET_err.InvalidDatabaseVersion)
		{
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00012CD4 File Offset: 0x00010ED4
		private EsentInvalidDatabaseVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
