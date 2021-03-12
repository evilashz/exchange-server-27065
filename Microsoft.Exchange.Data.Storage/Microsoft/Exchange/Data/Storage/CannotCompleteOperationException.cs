using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000719 RID: 1817
	[Serializable]
	public class CannotCompleteOperationException : StorageTransientException
	{
		// Token: 0x06004799 RID: 18329 RVA: 0x0013028D File Offset: 0x0012E48D
		public CannotCompleteOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x00130296 File Offset: 0x0012E496
		public CannotCompleteOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x001302A0 File Offset: 0x0012E4A0
		protected CannotCompleteOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
