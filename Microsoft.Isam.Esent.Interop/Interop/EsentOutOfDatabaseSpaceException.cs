using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	public sealed class EsentOutOfDatabaseSpaceException : EsentQuotaException
	{
		// Token: 0x06000875 RID: 2165 RVA: 0x000121EE File Offset: 0x000103EE
		public EsentOutOfDatabaseSpaceException() : base("Maximum database size reached", JET_err.OutOfDatabaseSpace)
		{
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00012200 File Offset: 0x00010400
		private EsentOutOfDatabaseSpaceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
