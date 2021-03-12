using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000717 RID: 1815
	[Serializable]
	public class AutoDAccessException : StoragePermanentException
	{
		// Token: 0x06004793 RID: 18323 RVA: 0x00130253 File Offset: 0x0012E453
		public AutoDAccessException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x0013025C File Offset: 0x0012E45C
		public AutoDAccessException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x00130266 File Offset: 0x0012E466
		protected AutoDAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
