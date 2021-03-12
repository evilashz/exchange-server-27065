using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000173 RID: 371
	[Serializable]
	public sealed class EsentTransTooDeepException : EsentUsageException
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x00012926 File Offset: 0x00010B26
		public EsentTransTooDeepException() : base("Transactions nested too deeply", JET_err.TransTooDeep)
		{
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00012938 File Offset: 0x00010B38
		private EsentTransTooDeepException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
