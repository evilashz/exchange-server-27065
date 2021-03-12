using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public sealed class EsentCannotDisableVersioningException : EsentUsageException
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x00012CA6 File Offset: 0x00010EA6
		public EsentCannotDisableVersioningException() : base("Cannot disable versioning for this database", JET_err.CannotDisableVersioning)
		{
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00012CB8 File Offset: 0x00010EB8
		private EsentCannotDisableVersioningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
