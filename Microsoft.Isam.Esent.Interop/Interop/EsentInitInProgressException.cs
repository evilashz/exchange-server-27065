using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000144 RID: 324
	[Serializable]
	public sealed class EsentInitInProgressException : EsentOperationException
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x00012402 File Offset: 0x00010602
		public EsentInitInProgressException() : base("Database engine is being initialized", JET_err.InitInProgress)
		{
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00012414 File Offset: 0x00010614
		private EsentInitInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
