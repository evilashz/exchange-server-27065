using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000761 RID: 1889
	[Serializable]
	public class PartialCompletionException : StoragePermanentException
	{
		// Token: 0x06004871 RID: 18545 RVA: 0x00131019 File Offset: 0x0012F219
		public PartialCompletionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00131023 File Offset: 0x0012F223
		protected PartialCompletionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
