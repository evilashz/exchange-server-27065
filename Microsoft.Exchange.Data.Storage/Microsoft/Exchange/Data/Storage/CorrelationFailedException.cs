using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000720 RID: 1824
	[Serializable]
	public class CorrelationFailedException : StoragePermanentException
	{
		// Token: 0x060047AD RID: 18349 RVA: 0x001303F4 File Offset: 0x0012E5F4
		public CorrelationFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x001303FD File Offset: 0x0012E5FD
		public CorrelationFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x00130407 File Offset: 0x0012E607
		protected CorrelationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
