using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class StoragePermanentException : LocalizedException
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x000673C0 File Offset: 0x000655C0
		public StoragePermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000673C9 File Offset: 0x000655C9
		public StoragePermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000673D3 File Offset: 0x000655D3
		protected StoragePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
