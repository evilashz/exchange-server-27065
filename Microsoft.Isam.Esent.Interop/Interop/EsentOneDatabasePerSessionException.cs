using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200020C RID: 524
	[Serializable]
	public sealed class EsentOneDatabasePerSessionException : EsentUsageException
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x000139E2 File Offset: 0x00011BE2
		public EsentOneDatabasePerSessionException() : base("Just one open user database per session is allowed (JET_paramOneDatabasePerSession)", JET_err.OneDatabasePerSession)
		{
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000139F4 File Offset: 0x00011BF4
		private EsentOneDatabasePerSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
