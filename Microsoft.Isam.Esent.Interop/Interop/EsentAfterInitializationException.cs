using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000201 RID: 513
	[Serializable]
	public sealed class EsentAfterInitializationException : EsentUsageException
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x000138AE File Offset: 0x00011AAE
		public EsentAfterInitializationException() : base("Cannot Restore after init.", JET_err.AfterInitialization)
		{
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000138C0 File Offset: 0x00011AC0
		private EsentAfterInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
