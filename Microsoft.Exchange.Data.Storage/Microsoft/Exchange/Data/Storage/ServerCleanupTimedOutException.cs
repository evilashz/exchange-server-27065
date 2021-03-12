using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200077B RID: 1915
	[Serializable]
	public class ServerCleanupTimedOutException : StorageTransientException
	{
		// Token: 0x060048CD RID: 18637 RVA: 0x00131883 File Offset: 0x0012FA83
		public ServerCleanupTimedOutException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0013188C File Offset: 0x0012FA8C
		public ServerCleanupTimedOutException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x00131896 File Offset: 0x0012FA96
		protected ServerCleanupTimedOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
