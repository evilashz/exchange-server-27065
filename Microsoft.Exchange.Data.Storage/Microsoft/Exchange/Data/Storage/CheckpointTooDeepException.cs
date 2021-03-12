using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200071B RID: 1819
	[Serializable]
	public class CheckpointTooDeepException : StorageTransientException
	{
		// Token: 0x0600479E RID: 18334 RVA: 0x001302BD File Offset: 0x0012E4BD
		public CheckpointTooDeepException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x001302C6 File Offset: 0x0012E4C6
		public CheckpointTooDeepException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x001302D0 File Offset: 0x0012E4D0
		protected CheckpointTooDeepException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
