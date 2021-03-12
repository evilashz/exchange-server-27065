using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200012A RID: 298
	[Serializable]
	public sealed class EsentTermInProgressException : EsentOperationException
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x0001212A File Offset: 0x0001032A
		public EsentTermInProgressException() : base("Termination in progress", JET_err.TermInProgress)
		{
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001213C File Offset: 0x0001033C
		private EsentTermInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
