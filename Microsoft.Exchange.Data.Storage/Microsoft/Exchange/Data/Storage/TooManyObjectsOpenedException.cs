using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000784 RID: 1924
	[Serializable]
	public class TooManyObjectsOpenedException : StorageTransientException
	{
		// Token: 0x060048E6 RID: 18662 RVA: 0x00131974 File Offset: 0x0012FB74
		public TooManyObjectsOpenedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x0013197D File Offset: 0x0012FB7D
		public TooManyObjectsOpenedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x00131987 File Offset: 0x0012FB87
		protected TooManyObjectsOpenedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
