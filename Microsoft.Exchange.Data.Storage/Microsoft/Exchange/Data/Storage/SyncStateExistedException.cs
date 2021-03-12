using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200077F RID: 1919
	[Serializable]
	public class SyncStateExistedException : ObjectExistedException
	{
		// Token: 0x060048D8 RID: 18648 RVA: 0x001318ED File Offset: 0x0012FAED
		public SyncStateExistedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x001318F6 File Offset: 0x0012FAF6
		public SyncStateExistedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x00131900 File Offset: 0x0012FB00
		protected SyncStateExistedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
