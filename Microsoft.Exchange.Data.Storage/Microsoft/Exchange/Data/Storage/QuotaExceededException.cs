using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200076A RID: 1898
	[Serializable]
	public class QuotaExceededException : StoragePermanentException
	{
		// Token: 0x06004899 RID: 18585 RVA: 0x00131403 File Offset: 0x0012F603
		public QuotaExceededException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0013140C File Offset: 0x0012F60C
		public QuotaExceededException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x00131416 File Offset: 0x0012F616
		protected QuotaExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
