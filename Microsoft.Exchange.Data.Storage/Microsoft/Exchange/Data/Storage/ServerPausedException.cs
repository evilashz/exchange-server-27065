using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200077C RID: 1916
	[Serializable]
	public class ServerPausedException : StorageTransientException
	{
		// Token: 0x060048D0 RID: 18640 RVA: 0x001318A0 File Offset: 0x0012FAA0
		public ServerPausedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x001318A9 File Offset: 0x0012FAA9
		public ServerPausedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x001318B3 File Offset: 0x0012FAB3
		protected ServerPausedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
